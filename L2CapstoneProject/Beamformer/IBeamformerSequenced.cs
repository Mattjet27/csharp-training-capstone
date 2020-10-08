using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2CapstoneProject.Beamformer
{
    interface IBeamformerSequenced : IBeamformer
    {
        /// <summary>
        /// Configures a sequence based on a list of PhaseAmplitudeOffset values (dynamic)
        /// </summary>
        void ConfigureSequence(List<PhaseAmplitudeOffset> offsets);
        /// <summary>
        /// Configures a sequence based on loading a pre-configured sequence (static)
        /// </summary>
        void ConfigureSequence(string sequence);
        /// <summary>
        /// Initiates a previously configured sequence
        /// </summary>
        void InitiateSequence();
        /// <summary>
        /// Aborts a running sequence
        /// </summary>
        void AbortSequence();
    }
}
