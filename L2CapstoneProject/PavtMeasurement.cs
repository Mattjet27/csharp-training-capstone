﻿using System;
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
        //init
        // Initialize the NIRfsg session
        public void InitSession(string resourceName)
        {
            instrSession = new RFmxInstrMX(resourceName, "");
        }

        //config SG and SA
        public void Configure(bool isSteppedBeamformer, double cwFrequency, double cwPower, decimal measurementLength, 
                                decimal measurementOffset, List<PhaseAmplitudeOffset> offsetList, string triggerSource, double segmentInterval)
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

            specAn.ConfigureRF("", cwFrequency, cwPower+(double)offsets.Max(), 0);

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

        //init measurement
        public void Initiate()
        {
            specAn.Initiate("", "");
        }

        //wait for meas complete

        // get results
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

        //cleanup
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
