using System;
using System.Linq;
using System.Collections.Generic;
using NationalInstruments.RFmx.InstrMX;
using NationalInstruments.RFmx.SpecAnMX;

namespace L2CapstoneProject
{
    class PavtMeasurement
    { 
        RFmxInstrMX instrSession;
        RFmxSpecAnMX specAn;
        int NumberOfSegments = 1;

        /// <summary>
        /// Initiates RFInstrMX session for acquisition
        /// </summary>
        /// <param name="resourceName">SA instrument name</param>
        public void InitSession(string resourceName)
        {
            instrSession = new RFmxInstrMX(resourceName, "");
        }

        /// <summary>
        /// Configures SA and PAvT measurement properties based on user inputs
        /// </summary>
        /// <param name="isSteppedBeamformer">determines whether measurement will be stepped or sequenced</param>
        /// <param name="centerFrequency">center frequency (Hz)</param>
        /// <param name="power">CW power level (dB)</param>
        /// <param name="measurementLength">measurement length (s)</param>
        /// <param name="measurementOffset">measurement offset (s)</param>
        /// <param name="offsetList">list of Phase and Amplitude Offsets determined by user.</param>
        /// <param name="triggerSource">measurement start trigger source</param>
        /// <param name="segmentInterval">length of DUT output segments (s)</param>
        public void Configure(bool isSteppedBeamformer, double centerFrequency, double power, decimal measurementLength, 
                                decimal measurementOffset, List<PhaseAmplitudeOffset> offsetList, string triggerSource,
                                double segmentInterval)
        {
            NumberOfSegments = offsetList.Count;
            decimal[] offsets = new decimal[NumberOfSegments];
            for (int i = 0; i < NumberOfSegments; i++)
            {
                offsets[i] = offsetList[i].Amplitude;
            }

            instrSession.SetLOLeakageAvoidanceEnabled("", RFmxInstrMXLOLeakageAvoidanceEnabled.False);
            specAn = instrSession.GetSpecAnSignalConfiguration();
            specAn.SelectMeasurements("", RFmxSpecAnMXMeasurementTypes.Pavt, true);         

            specAn.ConfigureRF("", centerFrequency, power+(double)offsets.Max(), 0);

            //configure triggering to line up with beamformer output
            specAn.ConfigureDigitalEdgeTrigger("", triggerSource, RFmxSpecAnMXDigitalEdgeTriggerEdge.Rising, 0, true);
            if (isSteppedBeamformer)
            {
                specAn.Pavt.Configuration.ConfigureMeasurementLocationType("", RFmxSpecAnMXPavtMeasurementLocationType.Trigger);
                specAn.Pavt.Configuration.ConfigureMeasurementInterval("", (double)measurementOffset, (double)measurementLength);
            }
            else
            {
                specAn.Pavt.Configuration.ConfigureMeasurementLocationType("", RFmxSpecAnMXPavtMeasurementLocationType.Time);
                specAn.Pavt.Configuration.ConfigureMeasurementStartTimeStep("", NumberOfSegments,
                  (double)measurementOffset, segmentInterval);

            }
            
            specAn.Pavt.Configuration.ConfigureMeasurementBandwidth("", 10.0e6);
        }

        /// <summary>
        /// Initiates PAvT measurement
        /// </summary>
        public void Initiate()
        {
            specAn.Initiate("", "");
        }

        /// <summary>
        /// Fetches and formats PAvT measurement results for display.
        /// </summary>
        /// <returns>List of phase and amplitude measurements corresponding to each segment 
        /// specified by the user in measurement configuration</returns> 
        public List<PhaseAmplitudeOffset> FetchResults()
        {
            double timeout = 10.0;
            List<PhaseAmplitudeOffset> resultList = new List<PhaseAmplitudeOffset>(NumberOfSegments);
            double[] meanRelativePhase = new double[NumberOfSegments];                          /* (deg) */
            double[] meanRelativeAmplitude = new double[NumberOfSegments];                      /* (dB) */
            double[] meanAbsolutePhase = new double[NumberOfSegments];                          /* (deg) */
            double[] meanAbsoluteAmplitude = new double[NumberOfSegments];                      /* (dBm) */

            specAn.Pavt.Results.FetchPhaseAndAmplitudeArray("", timeout, ref meanRelativePhase,
                                        ref meanRelativeAmplitude, ref meanAbsolutePhase, ref meanAbsoluteAmplitude);

            for (int i = 0; i < NumberOfSegments; i++)
            {
                resultList.Add(new PhaseAmplitudeOffset((decimal)meanRelativePhase[i], (decimal)meanRelativeAmplitude[i]));
            }
            return resultList;
        }

        /// <summary>
        /// Closes any existing measurement configurations and instruemtn sessions.S
        /// </summary>
        public void CloseSession()
        {
            if (specAn != null)
            {
                specAn.Dispose();
                specAn = null;
            }

            if (instrSession != null)
            {
                instrSession.Close();
                instrSession = null;
            }
        }
    }
}
