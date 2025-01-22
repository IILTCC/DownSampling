using System.Collections.Generic;

namespace DownSamplingLibary.LargestTriangle
{
    public class Bucket<PointType> where PointType:GraphPoint
    {
        private readonly List<PointType> _points;
        public PointType SavedPoint { get; set; }
        public Bucket(List<PointType> points)
        {
            _points = points;
        }
        public List<PointType> GetPoints()
        {
            return _points;
        }
        public GraphPoint GetCenter()
        {
            double xSum = 0;
            double ySum = 0;
            foreach (PointType point in _points)
            {
                xSum += point.X;
                ySum += point.Y;
            }
            return new GraphPoint(xSum/_points.Count,ySum/_points.Count);
        }
    }
}
