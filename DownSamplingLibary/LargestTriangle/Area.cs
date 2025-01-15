using System;
using System.Collections.Generic;

namespace DownSamplingLibary.LargestTriangle
{
    public static class Area
    {
        private static double GetArea(GraphPoint p1, GraphPoint p2, GraphPoint p3)
        {
            double item1 = p1.X * (p2.Y - p3.Y);
            double item2 = p2.X * (p3.Y - p1.Y);
            double item3 = p3.X * (p1.Y - p2.Y);
            return 0.5 * Math.Abs(item1+item2+item3);
        }
        public static GraphPoint GetLargestArea(GraphPoint point, Bucket bucket , GraphPoint center)
        {
            int maxIndex = 0;
            double maxValue = 0;
            List<GraphPoint> bucketPoints = bucket.GetPoints();
            for(int bucketIndex = 0; bucketIndex< bucketPoints.Count;bucketIndex++)
            {
                if(GetArea(point, bucketPoints[bucketIndex],center)>maxValue)
                {
                    maxIndex = bucketIndex;
                    maxValue = GetArea(point, bucketPoints[bucketIndex], center);
                }
            }
            return bucketPoints[maxIndex];
        }
            

    }
}
