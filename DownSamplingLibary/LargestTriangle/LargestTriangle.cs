using System.Collections.Generic;

namespace DownSamplingLibary.LargestTriangle
{
    public class LargestTriangle<PointType> where PointType:GraphPoint
    {
        public LargestTriangle() { }

        public List<PointType> ReducePoints(List<PointType> points, int desiredPoints)
        {
            if (points.Count < 3 || desiredPoints>points.Count)
                return null;
            List<Bucket<PointType>> buckets = PopulateBuckets(points,desiredPoints);
            for(int indxBucket = Consts.INGORE_FIRST_BUCKET;indxBucket < desiredPoints-Consts.INGORE_LAST_BUCKET;indxBucket++)
            {
                GraphPoint prevPoint = buckets[indxBucket - 1].SavedPoint;
                GraphPoint nextPoint = buckets[indxBucket + 1].GetCenter();
                buckets[indxBucket].SavedPoint = Area<PointType>.GetLargestArea(prevPoint, buckets[indxBucket], nextPoint);
            }
            return CombineBuckets(buckets);
        }

        public List<PointType> CombineBuckets(List<Bucket<PointType>> buckets)
        {
            List<PointType> retPoints = new List<PointType>();
            foreach (Bucket<PointType> bucket in buckets)
               retPoints.Add(bucket.SavedPoint);
            return retPoints;
        }
        private List<Bucket<PointType>> PopulateBuckets(List<PointType> points, int numberOfBuckets)
        {
            numberOfBuckets -= Consts.FIRST_LAST_BUCKET;
            int avg = (points.Count- Consts.FIRST_LAST_BUCKET) / numberOfBuckets;
            int remaining = (points.Count- Consts.FIRST_LAST_BUCKET) % numberOfBuckets; // used for divide evenly all points
            List<Bucket<PointType>> buckets = new List<Bucket<PointType>>();

            AddBucket(ref buckets, points[Consts.FIRST_BUCKET_POINT]);
            int pointOffset = Consts.REMOVE_FIRST_POINT;

            for(int bucketIndex = 0;bucketIndex < numberOfBuckets; bucketIndex++)
            {
                List<PointType> currentPoints = new List<PointType>();

                int currentSum = avg + (remaining > 0 ? 1 : 0);
                for (int pointIndex = 0; pointIndex< currentSum;pointIndex++)
                    currentPoints.Add(points[pointIndex+pointOffset]);

                pointOffset+=currentSum;
                remaining--;
                buckets.Add(new Bucket<PointType>(currentPoints));
            }
            AddBucket(ref buckets, points[points.Count-1]);

            return buckets;
        }
        private void AddBucket(ref List<Bucket<PointType>> buckets, PointType point)
        {
            Bucket<PointType> firstBucket = new Bucket<PointType>(new List<PointType> { point});
            firstBucket.SavedPoint = point;
            buckets.Add(firstBucket);
        }
    }
}
