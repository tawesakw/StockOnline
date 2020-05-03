using System;
using System.Collections.Generic;
using StockOnline.Models;

namespace StockOnline.Common
{
   public  class GFG : IComparer<FoodMini>
    {
        public int Compare(FoodMini x, FoodMini y)
        {

            if (x == null || y == null || x.Quantity == 0 || y.Quantity == 0)
            {
                return 0;
            }

            // "CompareTo()" method 
            return x.Quantity.CompareTo(y.Quantity);

        }
    }

}
