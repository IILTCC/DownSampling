using System;

namespace DownSamplingLibary
{
    public class GraphPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public GraphPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
        public string ToString()
        {
            return "x- " + X.ToString() + ", y- " + Y.ToString();
        }
        public double DistanceBetweenPoint(GraphPoint point)
        {
            return Math.Sqrt(Math.Pow(X-point.X,2)+Math.Pow(Y-point.Y,2));
        }
        public double GetSlopePoints(GraphPoint point)
        {
            return (point.Y - Y) / (point.X - X);
        }
        public double GetOffset(double m)
        {
            return Y - m *X;
        }
    }
}
