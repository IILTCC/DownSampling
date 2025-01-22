using System;

namespace DownSamplingLibary
{
    public class Line
    {
        public double M { get; set; }
        public double B { get; set; }

        public Line(double m,double b)
        {
            M = m;
            B = b;
        }

        public double DistanceFromPoint(GraphPoint point)
        {
            double topValue = Math.Abs(M*point.X-point.Y + B);
            double bottomValue = Math.Sqrt(Math.Pow(M,2)+Math.Pow(-1,2));
            return topValue / bottomValue;
        }
    }
}
