using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2CapstoneProject.Beamformer
{
    public static class ComplexMath
    {
        public class PolarPoint
        {
            public double Radius { get; set; }
            public double Angle { get; set; }
            public PolarPoint(double radius, double angle)
            {
                Radius = radius;
                Angle = angle;
            }
        }

        public static PolarPoint CartesianToPolar(double x, double y)
        {
            double radius = Math.Sqrt((x*x) + (y*y));
            double angle = Math.Atan2(y,x);
            return new PolarPoint(radius, angle);
        }

        public static Tuple<double, double> PolarToCartesian(double magnitude, double angle)
        {
            double x = magnitude;
            double y = angle;
            return Tuple.Create(x,y);
        }
    }
}
