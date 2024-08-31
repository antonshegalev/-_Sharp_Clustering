using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Security.Cryptography;

namespace ClassLibraryClustering
{
    public partial class ClassMath
    {
        public static bool RangeAB(int a, int b, int? minA = null, int? minB = null)
        {
            if (minA == null)
            {
                minA = a;
            }
            if (minB == null)
            {
                minB = b;
            }
            if (a < minA || b <= a || b < minB) return false;
            else return true;
        }
        public static bool MinMax(List<Double> dataList, out Double min, out Double max)
        {
            min = Double.MaxValue;
            max = Double.MinValue;

            if (dataList.Count > 0)
            {
                foreach (var item in dataList)
                {
                    max = Math.Max(max, item);
                    min = Math.Min(min, item);
                }
                return true;
            }
            else
            {
                min = 0;
                max = 0;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns>Список где к каждому столбцу свои значения min и max [0] - min, [1] - max </returns>
        public static List<List<Double>> MinMax(List<List<Double>> dataList)
        {
            Double min = Double.MaxValue;
            Double max = Double.MinValue;

            List<List<Double>> minMax = new List<List<Double>>(); // [0] - min, [1] - max
            
            if (dataList.Count > 0)
            {
                for (int i = 0; i < dataList.Count; i++)
                {
                    minMax.Add(new List<Double>() { dataList[i][0], dataList[i][0]} );
                }
                
                for (int i = 0; i < dataList.Count; i++)
                {
                    for (int j = 0; j < dataList[i].Count; j++)
                    {
                        if (dataList[i][j] < minMax[j][0])
                        {
                            minMax[j][0] = dataList[i][j];
                        }

                        if (dataList[i][j] > minMax[j][1])
                        {
                            minMax[j][1] = dataList[i][j];
                        }
                    }
                }
                return minMax;
            }
            else
            {
                return null;
            }
        }

        public static Double NormalizedData(Double dataElem, Double min = 2, Double max = 100, Double a = 1, Double b = 0)
        {
        //    Double c = a; b = c; a = b;
            return a + ( (dataElem - min) * (b - a) ) / (max - min);
        }


        /// <summary>
        ///  Нормализация в диапазон от b по a[b;a]
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="a">0 - по умолчанию</param>
        /// <param name="b">1 - по умолчанию</param>
        /// <returns></returns>
        public static List<Double> NormalizedData(List<Double> dataList, Double a = 1, Double b = 0)
        {
         //   Double c = a; b = c; a = b; 
            List<Double> resL = new List<Double>();
            Double min;
            Double max;
            bool res = ClassMath.MinMax(dataList, out min, out max);
            if (res)
            {
                foreach (var item in dataList)
                {
                    resL.Add(NormalizedData(item, min, max, a, b));
                }
                return resL;
            }
            else
            {
                return null;
            }

        }



        public static List<Double> Zscaling(List<Double> dataList)
        {
            if (dataList.Count > 0)
            {
                List<Double> zScaling = new List<double>();
                Double average = ClassMath.AverageArithmetic(dataList);
                Double standartDeviation = StandartDeviation(dataList);
                foreach (var item in dataList)
                {
                    zScaling.Add(((item - average) / standartDeviation));
                }
                return zScaling; 
            }
            else
            {
                return null;
            }
        }

        public static List<Double> Tballs(List<Double> dataList)
        {
            if (dataList.Count > 0)
            {
                List<Double> tBalls = new List<double>();
                foreach (var item in dataList)
                {
                    tBalls.Add(10 *item + 50);
                }
                return tBalls;
            }
            else
            {
                return null;
            }
        }

        public static Double StandartDeviation(List<Double> dataList)
        {
            if (dataList.Count>0)
            {
                Double average = ClassMath.AverageArithmetic(dataList);
                List<Double> lSt1 = new List<double>();
                foreach (var item in dataList)
                {
                    lSt1.Add(Math.Pow(item - average, 2));
                }


                return ClassMath.Sqrtn( lSt1.Sum()/(lSt1.Count - 1), 2); 
            }
            else
            {
                return 0;
            }
        }

        public static Double EuclideanDistance(List<Double> dataList, List<Double> centrList)
        {
            if (dataList.Count>0 && dataList.Count == centrList.Count)
            {
                Double summ = 0;
                for (int i = 0; i < dataList.Count; i++)
                {
                    summ += Math.Pow(dataList[i] - centrList[i],2);
                }

                return ClassMath.Sqrtn(summ,2); 
            }
            else
            {
                return 0;
            }
        }

        public static Double EuclideanDistance(Double data, Double centr)
        {
            if (data != 0 && centr != 0)
            {
                return ClassMath.Sqrtn(Math.Pow(data - centr, 2), 2);
            }
            else
            {
                return 0;
            }
        }

        public static Double ChebyshevDistance(List<Double> dataList, List<Double> centrList)
        {
            if (dataList.Count > 0 && dataList.Count == centrList.Count)
            {
                Double summ = 0;
                List<Double> lAbs = new List<Double>();

                for (int i = 0; i < dataList.Count; i++)
                {
                  lAbs.Add(Math.Abs(dataList[i] - centrList[i]));
                }

                return lAbs.Max();
            }
            else
            {
                return 0;
            }
        }

        public static Double ManhattanDistance(List<Double> dataList, List<Double> centrList)
        {
            if (dataList.Count > 0 && dataList.Count == centrList.Count)
            {
                Double summ = 0;
                List<Double> lAbs = new List<Double>();

                for (int i = 0; i < dataList.Count; i++)
                {
                    lAbs.Add(Math.Abs(dataList[i] - centrList[i]));
                }

                return lAbs.Sum();
            }
            else
            {
                return 0;
            }
        }
    }
}
