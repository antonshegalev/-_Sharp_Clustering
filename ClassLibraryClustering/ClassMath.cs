using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Security;

namespace ClassLibraryClustering
{
    partial class ClassMath
    {
        public static double TrimmedMean(List<Double> arr, Double p)
        {
            arr.Sort();
            Double summ = 0;
            Double trimmMean = 0;
            int up = (int) ( (double) arr.Count * p /2) ;
            int down = arr.Count - up;
            int sCount = 0;

            for (int i = up; i < down; i++)
            {
                summ += arr[i];
                sCount++;
            }
            trimmMean = summ / (double) sCount;

            return trimmMean;
        }

        public static double Sqrtn(double num, int count)
        {
            Double sqrn = Math.Pow(num, ((Double)1 / (Double)count));
            return sqrn;
        }

        public static double AverageGeometric(List<Double> array)
        {
            if (array.Count > 0)
            {
                Double composition = array[0];
                for (int k = 1; k < array.Count; k++)
                {
                    composition *= array[k];
                }

                Double aGeom = Sqrtn(composition, array.Count);
                return aGeom;
            }
            return 0;
        }

        /// <summary>
        ///  Среднее арифметическое
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static double AverageArithmetic(List<Double> array)
        {
            if (array.Count > 0)
            {
                Double summ = 0;
                for (int k = 0; k < array.Count; k++)
                {
                    summ += array[k];
                }
                return (summ / array.Count); 
            }
            return 0;
        }

        /// <summary>
        ///  Сглаженный тренд значений массива и его крайних значений с заданными весами (0.5 по умолчанию)
        /// </summary>
        /// <param name="dataList">Массив считаемых значений (минимум 2 значения) </param>
        /// <param name="weight1">Вес верхнего значения</param>
        /// <param name="weight2">Вес нижнего значения</param>
        public static double SmoothedTrend(List<Double> dataList, int period = 12, double weight1 = 0.5, double weight2 = 0.5)
        {
            double summ = 0;
            if (dataList.Count > period && period % 2 == 0)
            {
                for (int i = 1; i < period; i++)
                {
                    summ += dataList[i];
                }
                summ += dataList[0] * (weight1);
                summ += dataList[period] * (weight2);
                return summ / period;
            }
            else if (dataList.Count >= period)
            {
                for (int i = 0; i < period; i++)
                {
                    summ += dataList[i];
                }
                return summ / period;
            }
            return 0;
        }
        

    }
}
