using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2CapstoneProject.Beamformer
{
    public abstract class BeamformerBase : IBeamformerSequenced, IBeamformerStepped
    {
        public enum BeamformerType
        {
            Stepped,
            Sequeneced,
        }

        public enum SequeneceMode
        {
            None,
            Dynamic,
            Static,
        }

        public BeamformerType Type { get; set; }
        private SequeneceMode Mode { get; set; }

        public virtual void WriteOffset(PhaseAmplitudeOffset offset)
        {
            Mode = SequeneceMode.None;
        }

        public virtual void ConfigureSequence(List<PhaseAmplitudeOffset> offsets)
        {
           Mode = SequeneceMode.Dynamic;
        }

        public virtual void ConfigureSequence(string sequence)
        {
            Mode = SequeneceMode.Static;
        }

        public virtual void InitiateSequence()
        {
            throw new NotImplementedException();
        }

        public virtual void AbortSequence()
        {
            throw new NotImplementedException();
        }

        public abstract void Connect();

        public abstract void Disconnect();
    }
}
