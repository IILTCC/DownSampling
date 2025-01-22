using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownSamplingLibary.RamerDouglasPeucker
{
    public static class RamerDouglas
    {

        public static List<GraphPoint> ReducePoints(List<GraphPoint> points,double epsilon)
        {
            (int index, double value) furthestPoint = GetFurthestPoint(points);
            List<GraphPoint> retList = new List<GraphPoint>();

            if(furthestPoint.value>epsilon)
            {
                List<GraphPoint> left = new List<GraphPoint>();
                LoadPoints(points,ref left,Consts.LOAD_FROM_BEGGINING, furthestPoint.index);
                List<GraphPoint> right = new List<GraphPoint>();
                LoadPoints(points,ref right,furthestPoint.index, points.Count);

                foreach (GraphPoint point in ReducePoints(left, epsilon))
                    retList.Add(point);
                foreach (GraphPoint point in ReducePoints(right, epsilon))
                    retList.Add(point);
            }
            else
            {
                retList.Add(points[0]);
                retList.Add(points[points.Count-1]);
            }
            return retList;
        }
        private static (int indext,double value) GetFurthestPoint(List<GraphPoint> points)
        {
            GraphPoint p1 = points[Consts.FIRST_POINT];
            GraphPoint pn = points[points.Count - Consts.LAST_POINT];
            double slope = p1.GetSlopePoints(pn);
            Line line = new Line(slope, p1.GetOffset(slope));
            return FurthestPointFromLine(line, points);
        }
        private static void LoadPoints(List<GraphPoint>origin, ref List<GraphPoint>dist,int startIndex,int endIndex)
        {
            for(int index = startIndex;index<endIndex;index++)
                dist.Add(origin[index]);
        }
        public static (int index,double value) FurthestPointFromLine(Line line,List<GraphPoint> points)
        {
            int maxPointIndex = 0;
            double maxPointValue = 0;
            for(int index = 0;index < points.Count;index++)
            {
                if(line.DistanceFromPoint(points[index])>maxPointValue)
                {
                    maxPointIndex = index;
                    maxPointValue = line.DistanceFromPoint(points[index]);
                }
            }
            return (maxPointIndex,maxPointValue);
        }
    }
}
