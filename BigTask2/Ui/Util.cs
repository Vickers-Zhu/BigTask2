using System;
using System.Collections.Generic;
using System.Text;
using BigTask2.Api;

namespace BigTask2.Ui
{
    public class Util
    {
        public static double CostSum(IEnumerable<Route> routes)
        {
            double result = 0.0;
            foreach (Route route in routes)
            {
                result += route.Cost;
            }
            return result;
        }

        public static double TimeSum(IEnumerable<Route> routes)
        {
            double result = 0.0;
            foreach (Route route in routes)
            {
                result += route.TravelTime;
            }
            return result;
        }
    }
}
