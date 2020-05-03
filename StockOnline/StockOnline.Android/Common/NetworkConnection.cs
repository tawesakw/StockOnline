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
using StockOnline.Common;
using Android.Net;
using StockOnline.Droid.Common;

[assembly:Xamarin.Forms.Dependency(typeof(NetworkConnection))]
namespace StockOnline.Droid.Common
{
    
    public class NetworkConnection :INetworkConnection
    {

        public bool IsConnected { get; set; }

        public void CheckNetworkConnection() {
            var connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            if (activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting)
            {
                IsConnected = true;
            }
            else {
                IsConnected = false;
            } 

        }
    }
}