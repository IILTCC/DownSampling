using System.Collections.Generic;

namespace DownSamplingLibary.LargestTriangle
{
    public class Bucket
    {
        private readonly List<GraphPoint> _points;
        public GraphPoint SavedPoint { get; set; }
        public Bucket(List<GraphPoint> points)
        {
            _points = points;
        }
        public List<GraphPoint> GetPoints()
        {
            return _points;
        }
        public GraphPoint GetCenter()
        {
            double xSum = 0;
            double ySum = 0;
            foreach (GraphPoint point in _points)
            {
                xSum += point.X;
                ySum += point.Y;
            }
            return new GraphPoint(xSum/_points.Count,ySum/_points.Count);
        }
    }
}
