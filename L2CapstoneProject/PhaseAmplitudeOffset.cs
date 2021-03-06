﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L2CapstoneProject
{
    /// <summary>
    /// Dedicated type for handling Phase and Amplitude offset values.
    /// </summary>
    public class PhaseAmplitudeOffset
    {
        /// <summary>
        /// Phase value, in degrees
        /// </summary>
        public decimal Phase { get; set; }
        /// <summary>
        /// Amplitude value, in dB
        /// </summary>
        public decimal Amplitude { get; set; }

        public PhaseAmplitudeOffset()
        {
        }

        public PhaseAmplitudeOffset(decimal phase, decimal amplitude)
        {
            Phase = phase;
            Amplitude = amplitude;
        }

        internal string GetDisplayText()
        {
            return $";{Phase};{Amplitude}";
        }

        /// <summary>
        /// Gets the phase and amplitude offset values and formats them into a ListViewItem.
        /// </summary>
        /// <returns>A ListViewItem formated such that Phase is the first column element, followed by the Amplitude.</returns>
        internal ListViewItem GetDisplayItem()
        {
            return new ListViewItem(new string[] {Phase.ToString(), Amplitude.ToString()});
        }

        /// <summary>
        /// Gets the phase and amplitude offset values and formats them into a ListViewItem.
        /// </summary>
        /// <returns>A ListViewItem formated such that index is the first column element, followed by Phase, then Amplitude.</returns>
        internal ListViewItem GetDisplayItem(int index)
        {
            return new ListViewItem(new string[] { index.ToString(), Phase.ToString(), Amplitude.ToString() });
        }
    }
}
