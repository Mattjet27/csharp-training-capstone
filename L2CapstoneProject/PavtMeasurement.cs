using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.ModularInstruments.NIRfsg;
using NationalInstruments.RFmx.InstrMX;
using NationalInstruments.RFmx.SpecAnMX;
using NationalInstruments.ModularInstruments.SystemServices.DeviceServices;

namespace L2CapstoneProject
{
    class PavtMeasurement
    {
        NIRfsg rfsg; 
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
        public void ConfigureSA(bool isSteppedBeamformer, double cwFrequency, double cwPower, List<PhaseAmplitudeOffset> offsetList)
        {
            specAn = instrSession.GetSpecAnSignalConfiguration();
            specAn.SelectMeasurements("", RFmxSpecAnMXMeasurementTypes.Pavt, true);
            specAn.ConfigureRF("", cwFrequency, cwPower, 0);

            const int NumberOfSegments = 1;
            
            const int segmentStartTimeArraySize = 1;
            double[] segmentStartTime = new double[segmentStartTimeArraySize]; 
            double measurementOffset = 0.0; 
            double measurementLength = 1.0e-3;

            //configure triggering to line up with beamformer output
            specAn.ConfigureDigitalEdgeTrigger("", digitalEdgeSource, digitalEdge, triggerDelay, enableTrigger);

            if (isSteppedBeamformer)
            {
                specAn.Pavt.Configuration.ConfigureMeasurementLocationType("", RFmxSpecAnMXPavtMeasurementLocationType.Trigger);
                specAn.Pavt.Configuration.ConfigureNumberOfSegments("", NumberOfSegments);
            }
            else
            {
                specAn.Pavt.Configuration.ConfigureMeasurementLocationType("", RFmxSpecAnMXPavtMeasurementLocationType.Time);
                specAn.Pavt.Configuration.ConfigureNumberOfSegments("", segmentStartTimeArraySize);
                specAn.Pavt.Configuration.ConfigureSegmentStartTimeList("", segmentStartTime);
                
            }

            specAn.Pavt.Configuration.ConfigureMeasurementBandwidth("", measurementBandwidth);
            specAn.Pavt.Configuration.ConfigureMeasurementInterval("", measurementOffset, measurementLength);

        }

        //init measurement
        public void Initiate()
        {
            specAn.Initiate("", "");
        }

        //wait for meas complete

        // get results
        public void FetchResults()
        {
            double timeout = 10.0;
            double[] meanRelativePhase = new double[NumberOfSegments];                          /* (deg) */
            double[] meanRelativeAmplitude = new double[NumberOfSegments];                      /* (dB) */
            double[] meanAbsolutePhase = new double[NumberOfSegments];                          /* (deg) */
            double[] meanAbsoluteAmplitude = new double[NumberOfSegments];                      /* (dBm) */

            specAn.Pavt.Results.FetchPhaseAndAmplitudeArray("", timeout, ref meanRelativePhase,
               ref meanRelativeAmplitude, ref meanAbsolutePhase, ref meanAbsoluteAmplitude);

            for (int i = 0; i < NumberOfSegments; i++)
            {
                specAn.Pavt.Results.FetchPhaseTrace("", timeout, i, ref phase[i]);
                specAn.Pavt.Results.FetchAmplitudeTrace("", timeout, i, ref amplitude[i]);
            }
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
