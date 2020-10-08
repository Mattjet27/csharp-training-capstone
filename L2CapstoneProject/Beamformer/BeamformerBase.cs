using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2CapstoneProject.Beamformer
{
    public abstract class BeamformerBase : IBeamformerSequenced, IBeamformerStepped
    {
        public enum Mode
        {
            Dynamic,
            Static,
        }
        public Mode SequeneceMode { get; set; }

        public virtual void ConfigureSequence(List<PhaseAmplitudeOffset> offsets)
        {
            SequeneceMode = Mode.Dynamic;
        }

        public virtual void ConfigureSequence(string sequence)
        {
            SequeneceMode = Mode.Static;
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

        public abstract void WriteOffset(PhaseAmplitudeOffset offset);
    }
}
