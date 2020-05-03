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
using Firebase.Database;
using Firebase;
namespace StockOnline.Droid.Helper
{
    public static class AppDataHelper
    {
        public static FirebaseDatabase GetDatabase() {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseDatabase database=  null;
            /*
            if (app == null)
            {
                var option = new FirebaseOptions.Builder()
                    .SetApplicationId("xamarinfirebase-4f832")
                    .SetApiKey("AIzaSyBJUFo_fLKlY0zmeHeJtO3MoRibHFyt8Bw")
                    .SetDatabaseUrl("https://xamarinfirebase-4f832.firebaseio.com")
                    .SetStorageBucket("xamarinfirebase-4f832.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, option);
                database = FirebaseDatabase.GetInstance(app);
            }
            else
            {
                database = FirebaseDatabase.GetInstance(app);
            }
            */
            return database;

        }
    }
}