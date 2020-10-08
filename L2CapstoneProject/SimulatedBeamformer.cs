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
    public class SimulatedBeamformer : BeamformerBase
    {
        private readonly NIRfsg _rfsg = null;
        //private readonly NIDCPower _dcpwr = null;
        //private readonly NIDigital _digital = null;
        public double? Frequency { get; set; } = null;
        public double? Power { get; set; } = null; 

        public SimulatedBeamformer(NIRfsg rfsgHandle)
        {
            _rfsg = rfsgHandle;
        }

        public SimulatedBeamformer(NIRfsg rfsgHandle, double frequency, double power)
        {
            _rfsg = rfsgHandle;
            Frequency = frequency;
            Power = power;
        }

        public override void Connect()
        {
            // Power on DUT
            // Write inital DUT registers to configure it correctly.
            if (Frequency != null && Power != null)
            {
                // Configure the instrument 
                _rfsg.RF.Configure((double)Frequency, (double)Power);
                // Initiate Generation 
                _rfsg.Initiate();
                //throw new NotImplementedException();
            }
            else
            {
                throw new NullReferenceException();
            }

        }

        public override void Disconnect()
        {
            // Write any register values necessciary to power down DUT safely.
            
            // Power down the DUT.

            throw new NotImplementedException();
        }

        public override void WriteOffset(PhaseAmplitudeOffset offset)
        {
            // Write any register(s) necessiary to configure the provided Phase and Offset values.

            throw new NotImplementedException();
        }

        /*public override void InitiateSequence()
        {
            base.InitiateSequence();
        }
        public override void ConfigureSequence(List<PhaseAmplitudeOffset> offsets)
        {
            base.ConfigureSequence(offsets);
        }

        public override void ConfigureSequence(string sequence)
        {
            base.ConfigureSequence(sequence);
        }

        public override void AbortSequence()
        {
            base.AbortSequence();
        }
        */
        public override string ToString()
        {
            return "Simualted Beamformer";
        }
    }
}
