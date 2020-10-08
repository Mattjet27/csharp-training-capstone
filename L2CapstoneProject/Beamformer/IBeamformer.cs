using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2CapstoneProject.Beamformer
{
    public interface IBeamformer
    {
        /// <summary>
        /// Connect to the DUT (power on, write to register, etc.)
        /// </summary>
        void Connect();
        /// <summary>
        /// Disconnect from the DUT (write registers, power off, etc.)
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Writes a PhaseAmplitudeOffset to registers on the device.
        /// </summary>
        /// <param name="offset"></param>
        void WriteOffset(PhaseAmplitudeOffset offset);
    }
}
