using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace RimWorldAdvancedAIMod.AI.Algorithms
{
    public class KMeansClustering
    {
        public int K { get; private set; }
        public List<IntVec3> Centroids { get; private set; }
        public Dictionary<IntVec3, List<IntVec3>> Clusters { get; private set; }

        public KMeansClustering(int k)
        {
            K = k;
            Centroids = new List<IntVec3>(k);
            Clusters = new Dictionary<IntVec3, List<IntVec3>>();
        }

        public void InitializeCentroids(Map map)
        {
            Random rand = new Random();
            for (int i = 0; i < K; i++)
            {
                IntVec3 randomPoint = map.AllCells.RandomElementByWeight(cell => cell.Standable(map) ? 1 : 0);
                if (!Centroids.Contains(randomPoint))
                {
                    Centroids.Add(randomPoint);
                    Clusters[randomPoint] = new List<IntVec3>();
                }
                else
                {
                    i--; // Retry if the centroid is already selected
                }
            }
        }

        public void Cluster(Map map)
        {
            bool hasChanged;
            do
            {
                foreach (var cluster in Clusters.Values)
                {
                    cluster.Clear();
                }

                foreach (IntVec3 cell in map.AllCells)
                {
                    if (!cell.Standable(map)) continue; // Skip non-standable cells

                    IntVec3 nearestCentroid = GetNearestCentroid(cell);
                    Clusters[nearestCentroid].Add(cell);
                }

                hasChanged = UpdateCentroids(map);
            }
            while (hasChanged);
        }

        private IntVec3 GetNearestCentroid(IntVec3 cell)
        {
            IntVec3 nearest = default;
            double minDistance = double.MaxValue;

            foreach (IntVec3 centroid in Centroids)
            {
                double distance = cell.DistanceToSquared(centroid);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = centroid;
                }
            }

            return nearest;
        }

        private bool UpdateCentroids(Map map)
        {
            bool hasChanged = false;
            for (int i = 0; i < Centroids.Count; i++)
            {
                IntVec3 oldCentroid = Centroids[i];
                if (Clusters[oldCentroid].Count == 0) continue;

                IntVec3 newCentroid = CalculateMeanPosition(Clusters[oldCentroid]);
                if (!newCentroid.Equals(oldCentroid))
                {
                    Centroids[i] = newCentroid;
                    hasChanged = true;
                }
            }

            return hasChanged;
        }

        private IntVec3 CalculateMeanPosition(List<IntVec3> cluster)
        {
            int x = 0, z = 0;
            foreach (IntVec3 pos in cluster)
            {
                x += pos.x;
                z += pos.z;
            }
            return new IntVec3(x / cluster.Count, 0, z / cluster.Count);
        }
    }
}
