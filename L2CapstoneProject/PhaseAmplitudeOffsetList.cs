using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2CapstoneProject
{
    public class PhaseAmplitudeOffsetList : List<PhaseAmplitudeOffset>
    {
        /// <summary>
        /// Private list of hase and Amplitude offset values. 
        /// </summary>
        private List<PhaseAmplitudeOffset> offsetList;
        /// <summary>
        /// Create a custom list to keep track of Phase and Amplitude offset values of type PhaseAmplitudeOffset.
        /// </summary>
        public PhaseAmplitudeOffsetList()
        {
            offsetList = new List<PhaseAmplitudeOffset>();
        }
    }
}
