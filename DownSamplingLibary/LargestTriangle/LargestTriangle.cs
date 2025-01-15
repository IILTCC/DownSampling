using System.Collections.Generic;

namespace DownSamplingLibary.LargestTriangle
{
    public class LargestTriangle
    {
        public LargestTriangle() { }

        public List<GraphPoint> ReducePoints(List<GraphPoint> points, int desiredPoints)
        {
            if (points.Count < 3 || desiredPoints>points.Count)
                return null;
            List<Bucket> buckets = PopulateBuckets(points,desiredPoints);
            for(int indxBucket = Consts.INGORE_FIRST_BUCKET;indxBucket < desiredPoints-Consts.INGORE_LAST_BUCKET;indxBucket++)
            {
                GraphPoint prevPoint = buckets[indxBucket - 1].SavedPoint;
                GraphPoint nextPoint = buckets[indxBucket + 1].GetCenter();
                buckets[indxBucket].SavedPoint = Area.GetLargestArea(prevPoint, buckets[indxBucket], nextPoint);
            }
            return CombineBuckets(buckets);
        }

        public List<GraphPoint> CombineBuckets(List<Bucket> buckets)
        {
            List<GraphPoint> retPoints = new List<GraphPoint>();
            foreach (Bucket bucket in buckets)
               retPoints.Add(bucket.SavedPoint);
            return retPoints;
        }
        private List<Bucket> PopulateBuckets(List<GraphPoint> points, int numberOfBuckets)
        {
            numberOfBuckets -= Consts.FIRST_LAST_BUCKET;
            int avg = (points.Count- Consts.FIRST_LAST_BUCKET) / numberOfBuckets;
            int remaining = (points.Count- Consts.FIRST_LAST_BUCKET) % numberOfBuckets; // used for divide evenly all points
            List<Bucket> buckets = new List<Bucket>();

            AddBucket(ref buckets, points[Consts.FIRST_BUCKET_POINT]);
            int pointOffset = Consts.REMOVE_FIRST_POINT;

            for(int bucketIndex = 0;bucketIndex < numberOfBuckets; bucketIndex++)
            {
                List<GraphPoint> currentPoints = new List<GraphPoint>();

                int currentSum = avg + (remaining > 0 ? 1 : 0);
                for (int pointIndex = 0; pointIndex< currentSum;pointIndex++)
                    currentPoints.Add(points[pointIndex+pointOffset]);

                pointOffset+=currentSum;
                remaining--;
                buckets.Add(new Bucket(currentPoints));
            }
            AddBucket(ref buckets, points[points.Count-1]);

            return buckets;
        }
        private void AddBucket(ref List<Bucket> buckets, GraphPoint point)
        {
            Bucket firstBucket = new Bucket(new List<GraphPoint> { point});
            firstBucket.SavedPoint = point;
            buckets.Add(firstBucket);
        }
    }
}
