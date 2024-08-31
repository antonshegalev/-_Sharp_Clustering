using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryClustering
{
    public class ClassClustering
    {
        private List<List<Double>> inputData; // = new List<List<Double>>();
        private List<List<Double>> centerCluster; // = new List<List<Double>>();
        public List<List<Double>> newCenterCluster = new List<List<double>>();



        public List<List<List<Double>>> chronologyCenter = new List<List<List<Double>>>();

        public List<List<Double>> chronologyDistance = new List<List<Double>>();


        private int countCenter;

        private List<List<Double>> distance         = new List<List<Double>>(); // 
        private List<Double> averageDistanceCluster = new List<Double>(); // 
        public List<long> indexCluster             = new List<long>(); // 

        private byte measureDistance;


        public ClassClustering(List<List<Double>> _inputData, List<List<Double>> _centerCluster)
        {
            inputData     = _inputData;
            centerCluster = _centerCluster;
            countCenter   = _centerCluster.Count;
            chronologyCenter.Add(_centerCluster);
        }

        /// <summary>
        /// Использовать только в тестах.
        /// </summary>
        public List<List<Double>> Distance
        {
            get
            {
                if (inputData.Count > 0)
                {
                    distance.Clear();
                    for (int i = 0; i < inputData.Count; i++)
                    {
                        List<Double> distanceRow = new List<Double>();
                        for (int j = 0; j < centerCluster.Count; j++)
                        {
                            Double _distance;

                            switch (measureDistance)
                            {
                                case 0: _distance = ClassMath.EuclideanDistance(inputData[i], centerCluster[j]); break;
                                case 1: _distance = ClassMath.ManhattanDistance(inputData[i], centerCluster[j]); break;
                                case 2: _distance = ClassMath.ChebyshevDistance(inputData[i], centerCluster[j]); break;
                                default: _distance = ClassMath.EuclideanDistance(inputData[i], centerCluster[j]); break;
                            }

                            distanceRow.Add(_distance);
                        }
                        distance.Add(distanceRow);
                    }
                    return distance;
                }
                else return null;
            }
        }

        /// <summary>
        ///  Использовать только в тестах.  
        /// </summary>
        public List<long> IndexCluster
        {
            get
            {
                if (distance == null || distance.Count <= 0)
                {
                    List<List<Double>> dist =  this.Distance;
                }

                indexCluster.Clear();
                if (distance != null || distance.Count > 0)
                {
                    foreach (var item in distance)
                    {
                        if (item.Count > 1)
                        {
                            Double min = item[0];
                            long indexMin = 0;
                            for (int i = 1; i < item.Count; i++)
                            {
                                if (item[i] < min)
                                {
                                    min = item[i];
                                    indexMin = i;
                                }
                            }
                            indexCluster.Add(indexMin);
                        } else indexCluster.Add(0);
                    }
                    return indexCluster;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Использовать только в тестах.
        /// </summary>
        public List<List<Double>> NewCenterCluster
        {
            get
            {
                if (indexCluster == null || indexCluster.Count == 0)
                {
                    List<long> indexCluster = this.IndexCluster;
                }

                if (indexCluster.Count> 0)
                {
                    newCenterCluster.Clear();
                    newCenterCluster = new List<List<Double>>();
                    for (int i = 0; i < centerCluster.Count; i++)
                    {
                        List<Double> newCenterClusterRow = new List<Double>();
                        for (int j = 0; j < centerCluster[i].Count; j++)
                        {
                            newCenterClusterRow.Add(0);
                        }
                        newCenterCluster.Add(newCenterClusterRow);
                    }
                    
                    for (int c = 0; c < newCenterCluster.Count; c++)
                    {
                        for (int i = 0; i < inputData.Count; i++)
                        {
                            if (indexCluster[i] == c)
                            {
                                for (int j = 0; j < inputData[i].Count; j++)
                                {
                                    newCenterCluster[c][j] += inputData[i][j];
                                }
                            }
                        }
                    }

                    for (int i = 0; i < newCenterCluster.Count; i++)
                    {
                        for (int j = 0; j < newCenterCluster[i].Count; j++)
                        {
                            int count = indexCluster.Count(d => d == i);
                            newCenterCluster[i][j] = newCenterCluster[i][j] / count;
                        }
                    }

                    return newCenterCluster;
                } else return null;
            }
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="countIteration"></param>
        /// <param name="_measureDistance"> 0 - Euclidean; 1 - Manhattan; 2 - Chebyshev</param>
        /// <returns></returns>
        public List<List<Double>> FindCenterCluster(int? countIteration = null, byte _measureDistance = 0)
        {
            measureDistance = _measureDistance;
            for (int i = 0; i < (countIteration != null ? countIteration : int.MaxValue); i++)
            {
                List<List<Double>> dist = this.Distance;
                List<long> indexCluster = this.IndexCluster;
                List<List<Double>> newCenterCluster = this.NewCenterCluster;
                List<Double> chDist = new List<double>();
                Boolean findCenter = true;
                for (int k = 0; k < centerCluster.Count; k++)
                {
                    List<Double> centerClusterI = centerCluster[k];
                    List<Double> newCenterClusterI = newCenterCluster[k];

                    Double _distance;

                    switch (measureDistance)
                    {
                        case 0: _distance = ClassMath.EuclideanDistance(centerClusterI, newCenterClusterI); break;
                        case 1: _distance = ClassMath.ManhattanDistance(centerClusterI, newCenterClusterI); break;
                        case 2: _distance = ClassMath.ChebyshevDistance(centerClusterI, newCenterClusterI); break;
                        default: _distance = ClassMath.EuclideanDistance(centerClusterI, newCenterClusterI); break;
                    }
                    chDist.Add(_distance);

                    if (_distance != 0)  // Вычисление
                    {
                        findCenter = false;
                      //  break;
                    } 
                }
                chronologyDistance.Add(chDist);
                if (findCenter) break; 
                centerCluster = new List<List<double>>(newCenterCluster);
                chronologyCenter.Add(centerCluster);
            }
            return newCenterCluster;
        }

        public Double CheckSKO()
        {
            averageDistanceCluster.Clear();
            List<Double> skoCluster = new List<Double>();
            
            for (int i = 0; i < distance[0].Count; i++)
            {
                averageDistanceCluster.Add(0);
            }

            for (int c = 0; c < averageDistanceCluster.Count; c++)
            {
                for (int i = 0; i < distance.Count; i++)
                {
                    if (indexCluster[i] == c)
                    {
                       averageDistanceCluster[c] += distance[i][c];
                    }
                }
            }

            for (int j = 0; j < averageDistanceCluster.Count; j++)
            {
                int count = indexCluster.Count(d => d == j);
                averageDistanceCluster[j] = averageDistanceCluster[j] / count;
            }

            for (int c = 0; c < averageDistanceCluster.Count; c++)
            {
                for (int i = 0; i < distance.Count; i++)
                {
                    if (indexCluster[i] == c)
                    {
                        skoCluster.Add(Math.Pow(distance[i][c] - averageDistanceCluster[c], 2));
                    }
                }
            }
            
            return skoCluster.Sum();
        }
    }
}
