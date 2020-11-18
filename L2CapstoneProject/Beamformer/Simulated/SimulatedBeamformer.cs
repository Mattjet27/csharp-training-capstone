using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.ModularInstruments.NIRfsg;
using System.Reflection;

namespace L2CapstoneProject.Beamformer
{
    public class SimulatedBeamformer : BeamformerBase
    {
        private readonly NIRfsg _rfsg = null;
        readonly double iqRate;
        double cwOutputPower, maxOutputPwr;
        const double ArbSignalBandwidth = 1;
        string script;
        readonly RfsgRFPowerLevelType powerLevelType;
        readonly RfsgMarkerEventExportedOutputTerminal outputTerminal;
        // Create list for storing RF waveforms 
        List<RFWaveform> waveforms = new List<RFWaveform>();

        public SimulatedBeamformer(NIRfsg rfsgHandle, BeamformerType type)
        {
            _rfsg = rfsgHandle;
            base.Type = type;
            powerLevelType = RfsgRFPowerLevelType.PeakPower;
            outputTerminal = RfsgMarkerEventExportedOutputTerminal.PxiTriggerLine0;
            iqRate = _rfsg.Arb.IQRate;
            cwOutputPower = _rfsg.RF.PowerLevel;
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
            base.ConfigureSequence(offsets);
            cwOutputPower = _rfsg.RF.PowerLevel;
            // Get the maximum output power required for generating the offsets in the sequence
            maxOutputPwr = GetMaxOutputPwr(offsets, cwOutputPower);            
            // Build rf waveforms
            waveforms = BuildWaveformList(offsets);
            // Build Script based on Waveforms in list. 
            script = BuildScript(waveforms);
        }

        /*
        //public override void ConfigureSequence(string sequence)
        {
            base.ConfigureSequence(sequence);
            // Get the maximum output power required for generating the offsets in the sequence
            maxOutputPwr = GetMaxOutputPwr(sequence);            
            // Build rf waveforms
            waveforms = BuildWaveformList(sequence, maxOutputPwr);
            // Build Script based on Waveforms in list. 
            script = BuildScript(waveforms);
        }
        */

        public override void AbortSequence()
        {
            // Fire software scripttrigger to stop sequence. 
            _rfsg.Triggers.ScriptTriggers[0].SendSoftwareEdgeTrigger();
            // TODO add code to return the RSFG output back to the orignal CW mode?
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
        public double GetMaxOutputPwr(List<PhaseAmplitudeOffset> offsets, double cwOutputPower)
        {
            // Get the currently configured output power
            double maxOutputPower = cwOutputPower;
            // Find the max amplitude within the set of Amplitudes.
            foreach (PhaseAmplitudeOffset offset in offsets)
            {
                maxOutputPower = Math.Max((double)offset.Amplitude, maxOutputPower);
            }
            return maxOutputPower;
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

        internal static double CalculateRadius(double cwOutputPower, double maxOutputPwr, double amplitudeOffset)
        {
            return (DBmtoVrms(cwOutputPower) + DBmtoVrms(amplitudeOffset)) / DBmtoVrms(maxOutputPwr);
        }

        private static double DBmtoVrms(double dbmPower)
        {
            return Math.Sqrt(50 / 1000) * Math.Pow(10, dbmPower / 20);
        }

        private Tuple<double[], double[]> GenerateIQData(PhaseAmplitudeOffset offset, 
            double cwOutputPower, double maxOutputPwr, int numberOfSamples=64)
        {
            // Create I/Q data value arrays where 1 is the max output power from the set of Offset Amplitudes.
            double[] iData = new double[numberOfSamples];
            double[] qData = new double[numberOfSamples];
            ComplexMath.PolarPoint polarPoint = ComplexMath.CartesianToPolar(1.0, 0.0);
            Tuple<double, double> cartesianPoint = Tuple.Create(1.0, 0.0);

            // Add offsets.
            if ((cwOutputPower + (double)offset.Amplitude) < maxOutputPwr)
            {
                polarPoint.Radius = CalculateRadius(cwOutputPower, maxOutputPwr, (double)offset.Amplitude);
                polarPoint.Angle += ComplexMath.DegreesToRadians((double)offset.Phase);
                cartesianPoint = ComplexMath.PolarToCartesian(polarPoint.Radius, polarPoint.Angle);
            }
            else if ((cwOutputPower + (double)offset.Amplitude) > maxOutputPwr)
            {
                // TODO add better exception?
                throw new Exception($"Max Output Power {maxOutputPwr} was not calculated correctly.");
            }

            // Generate I and Q data arrays. 
            for (int sampleItr = 0; sampleItr < numberOfSamples; sampleItr++)
            {
                iData[sampleItr] = cartesianPoint.Item1;
                qData[sampleItr] = cartesianPoint.Item2;
            }
            // Return I Q data arrays as Tuple.
            return Tuple.Create(iData, qData);
        }

        private List<RFWaveform> BuildWaveformList(List<PhaseAmplitudeOffset> offsets)
        {
            Tuple<double[], double[]> iqData;
            // Create list for storing RF waveforms 
            List<RFWaveform> waveformlist = new List<RFWaveform>();
            // Setup a waveform object that captures the Amplitude and Phase information,
            // as well as calculates the coresponding I and Q data arrays.                     
            foreach (PhaseAmplitudeOffset offset in offsets)
            {
                RFWaveform waveform = new RFWaveform($"{offset.Phase}_{offset.Amplitude}"); // waveform name set based on Amplitude & Phase values.  
                waveform.AmplitudeOffset = offset.Amplitude;
                waveform.PhaseOffset = offset.Phase;
                iqData = GenerateIQData(offset, cwOutputPower, maxOutputPwr); // Calucalte IQ data arrays. 
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
