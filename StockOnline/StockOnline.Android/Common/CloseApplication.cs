﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StockOnline.Common;
using Xamarin.Forms;

namespace StockOnline.Droid.Common
{
    public class CloseApplication : IExitAppInterface
    {
        [Obsolete]
        public void closeApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}