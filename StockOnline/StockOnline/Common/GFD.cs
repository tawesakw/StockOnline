using System;
using System.Collections.Generic;
using StockOnline.Models;

namespace StockOnline.Common
{
   public  class GFD : IComparer<FoodMini>
    {
        public int Compare(FoodMini x, FoodMini y)
        {

            if (x == null || y == null || x.OrderDate == null || y.OrderDate == null)
            {
                return 0;
            }

            // "CompareTo()" method 
            return y.OrderDate.CompareTo(x.OrderDate);

        }
    }

}
