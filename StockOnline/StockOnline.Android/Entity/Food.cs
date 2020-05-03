using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace StockOnline.Droid.Entity
{
    public class Food
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public string NameEn { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }

        public string Details { get; set; }


        public Decimal CostPerUnit { get; set; }

        public int Quantity { get; set; }

    }
}