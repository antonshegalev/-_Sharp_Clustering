using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryClustering
{
    public class ClassElements
    {
        public struct elemArr
        {
            public String indexStr;
            public DateTime date;
            public int t;
            public Double data;


            public elemArr(String _indexStr, DateTime _date, int _t, Double _data)
            {
                indexStr = _indexStr;
                data = _data;
                t = _t;
                date = _date;
            }
        }

        /// <summary>
        ///  Seasonal and random component
        /// </summary>
        public struct elemCoef
        {
            public int indexElemArr;
            public Double smoothedTrend;
            public Double seasonalRandomComponent;  //Seasonal and random component

            public elemCoef(int _indexElemArr, Double _smoothedTrend, Double _seasonalRandomComponent)
            {
                indexElemArr = _indexElemArr;
                smoothedTrend = _smoothedTrend;
                seasonalRandomComponent = _seasonalRandomComponent;
            }
        }
    }
}
