using L2CapstoneProject.Beamformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.ModularInstruments.NIRfsg;
using System.Reflection;
//using NationalInstruments.ModularInstruments.NIDCPower;
//using NationalInstruments.ModularInstruments.NIDigital;

namespace L2CapstoneProject
{
    class RFWaveform
    {
        public string Name { get; }
        public decimal Amplitude { get; set; }
        public decimal Phase { get; set; }
        public double[] Idata { get; set; }
        public double[] Qdata { get; set; }
        public RFWaveform(string name)
        {
            Name = name;
        }
    }

    public class SimulatedBeamformer : BeamformerBase
    {
        private readonly NIRfsg _rfsg = null;
        //private readonly NIDCPower _dcpwr = null;
        //private readonly NIDigital _digital = null;
        double iqRate;
        RfsgRFPowerLevelType powerLevelType;
        string script;
        RfsgMarkerEventExportedOutputTerminal outputTerminal;
        const double ArbSignalBandwidth = 1;
        // Create list for storing RF waveforms 
        List<RFWaveform> waveforms = new List<RFWaveform>();
        double maxOutputPwr;

        public SimulatedBeamformer(NIRfsg rfsgHandle)
        {
            _rfsg = rfsgHandle;
        }

        public override void Connect()
        {
            // Power on DUT
            // Write inital DUT registers to configure it correctly.
            //throw new NotImplementedException();
        }

        public override void Disconnect()
        {
            // Write any register values necessciary to power down DUT safely.            
            // Power down the DUT.
            //throw new NotImplementedException();
        }

        public override void WriteOffset(PhaseAmplitudeOffset offset)
        {
            // Write any register(s) necessiary to configure the provided Phase and Offset values.
            //throw new NotImplementedException();
        }

        public override void InitiateSequence()
        {
            // Stop generation
            _rfsg.Abort();

            // Update the max output power, if required.
            SetMaxOutputPwr(maxOutputPwr);

            // Configure the power level type 
            _rfsg.RF.PowerLevelType = powerLevelType;

            // Configure the generation mode to Script 
            _rfsg.Arb.GenerationMode = RfsgWaveformGenerationMode.Script;

            // Export the marker event to the desired output terminal 
            _rfsg.DeviceEvents.MarkerEvents[0].ExportedOutputTerminal = outputTerminal;

            // Configure Script Trigger
            _rfsg.Triggers.ScriptTriggers[0].ConfigureSoftwareTrigger();

            // Configure the IQ rate of the waveforms 
            _rfsg.Arb.IQRate = iqRate;

            // Configure the signal bandwidth 
            _rfsg.Arb.SignalBandwidth = ArbSignalBandwidth;

            // Write the DC arb waveform(s)
            foreach (RFWaveform waveform in waveforms)
            {
                _rfsg.Arb.WriteWaveform(waveform.Name, waveform.Idata, waveform.Qdata);
            }

            // Write the script 
            _rfsg.Arb.Scripting.WriteScript(script);

            // Initiate Generation 
            _rfsg.Initiate();
        }
        public override void ConfigureSequence(List<PhaseAmplitudeOffset> offsets)
        {
            // Get the maximum output power required for generating the offsets in the sequence
            maxOutputPwr = GetMaxOutputPwr(offsets);            
            // Build rf waveforms
            waveforms = BuildWaveformList(offsets, maxOutputPwr);
            // Build Script based on Waveforms in list. 
            script = BuildScript(waveforms);
        }

        /*
        //public override void ConfigureSequence(string sequence)
        {
            GenerateIQData(offsets);
            BuildScript();
        }
        */

        public override void AbortSequence()
        {
            // Fire software scripttrigger to stop sequence. 
            _rfsg.Triggers.ScriptTriggers[0].SendSoftwareEdgeTrigger();
        }

        public override string ToString()
        {
            return "Simualted Beamformer";
        }

        #region Helper Functions
        /// <summary>
        /// The is method calculates the max output power required to generate the all the provided offset amplitudes. 
        /// </summary>
        /// <param name="offsets"></param>
        /// <returns>The max output power that the RFSG driver needs to be configured to,
        /// based on the provided list of offset amplitudes.
        /// a seperate method will update the driver session if this value ends up 
        /// being greater than what is currently configured within the driver session.
        /// </returns>
        /// 
        public double GetMaxOutputPwr(List<PhaseAmplitudeOffset> offsets)
        {
            // Get the currently configured output power
            decimal maxOutputPower = (decimal)_rfsg.RF.PowerLevel;
            // Find the max amplitude within the set of Amplitudes.
            foreach (PhaseAmplitudeOffset offset in offsets)
            {
                maxOutputPower = Math.Max(offset.Amplitude, maxOutputPower);
            }
            return (double)maxOutputPower;
        }

        /// <summary>
        /// This method will update the RFSG driver session if the max output value
        /// ends up being greater than what is currently configured within the 
        /// exisiting RFSG driver session.
        /// </summary>
        /// <param name="maxOutputPower"></param>
        public void SetMaxOutputPwr(double maxOutputPower)
        {
            // Get the currently configured output power
            double currentOutputPower = _rfsg.RF.PowerLevel;
            // Rasie the output power if the required max power level is greater than what is currently configured.
            if (currentOutputPower < maxOutputPower)
            {
                _rfsg.RF.PowerLevel = maxOutputPower;
            }
        }

        private Tuple<double[], double[]> GenerateIQData(PhaseAmplitudeOffset offset, double maxOutputPwr, int numberOfSamples=64)
        {
            // Create I/Q data value arrays where 1 is the max output power from the set of Offset Amplitudes.
            double[] iData = new double[numberOfSamples];
            double[] qData = new double[numberOfSamples];
            // Generate I and Q data arrays 
            for (int sampleItr = 0; sampleItr < numberOfSamples; sampleItr++)
            {
                iData[sampleItr] = 1.0;
                qData[sampleItr] = 0.0;
            }
            //
            return Tuple.Create(iData, qData);
        }

        private List<RFWaveform> BuildWaveformList(List<PhaseAmplitudeOffset> offsets, double maxOutputPwr)
        {
            Tuple<double[], double[]> iqData;
            // Create list for storing RF waveforms 
            List<RFWaveform> waveformlist = new List<RFWaveform>();
            // Setup a waveform object that captures the Amplitude and Phase information,
            // as well as calculates the coresponding I and Q data arrays.                     
            foreach (PhaseAmplitudeOffset offset in offsets)
            {
                RFWaveform waveform = new RFWaveform($"{offset.Phase}_{offset.Amplitude}"); // waveform name set based on Amplitude & Phase values.  
                waveform.Amplitude = offset.Amplitude;
                waveform.Phase = offset.Phase;
                iqData = GenerateIQData(offset, maxOutputPwr); // Calucalte IQ data arrays. 
                waveform.Idata = iqData.Item1;
                waveform.Qdata = iqData.Item2;
                waveformlist.Add(waveform); // Add the new waveform object to the waveforms list.
            }
            return waveformlist;
        }

        private string BuildScript(List<RFWaveform> waveforms)
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("script myScript");
            script.AppendLine("\trepeat until scriptTrigger0");
            foreach (RFWaveform waveform in waveforms)
            {
                script.AppendLine($"\t\tgenerate {waveform.Name} marker0(4)");
            }
            script.AppendLine("\tend repeat");
            script.Append("end script");
            return script.ToString();
        }
        #endregion
    }
}
