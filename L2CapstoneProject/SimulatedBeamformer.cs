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
<<<<<<< HEAD
            //throw new NotImplementedException();
=======
            throw new NotImplementedException();
>>>>>>> 6782440100684fa443c73c0d79541936533aa503
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
