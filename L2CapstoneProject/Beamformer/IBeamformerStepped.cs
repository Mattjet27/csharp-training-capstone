using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2CapstoneProject.Beamformer
{
    interface IBeamformerStepped : IBeamformer
    {
        /// <summary>
        /// Writes a PhaseAmplitudeOffset to registers on the device.
        /// </summary>
        /// <param name="offset"></param>
        void WriteOffset(PhaseAmplitudeOffset offset);
    }
}
