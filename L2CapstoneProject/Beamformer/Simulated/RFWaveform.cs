using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2CapstoneProject.Beamformer
{
    class RFWaveform
    {
        public string Name { get; set; }
        public decimal AmplitudeOffset { get; set; }
        public decimal PhaseOffset { get; set; }
        public double[] Idata { get; set; }
        public double[] Qdata { get; set; }
        public RFWaveform(string name)
        {
            Name = name;
        }
    }
}
