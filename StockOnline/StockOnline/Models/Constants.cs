using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace StockOnline.Models
{
    class Constants
    {
        public static bool isDev = true;
        public static Color BackgroundColor = Color.FromRgb(204,255,255);
        public static Color MainTextColor = Color.Black;
        public static int LoginIconHeight = 120;
        

        // Login -----------------------

        public static string LoginUrl = "https://test.com/api/Auth/Login";

        public static string NoInternetText = "No Integnet, please reconnect";
    }
}
