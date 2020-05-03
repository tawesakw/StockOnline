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
using StockOnline.Droid.Entity;
using Java.Util;

using SupportV7 = Android.Support.V7.App;
using Firebase.Database;
using StockOnline.Droid.Helper;
using StockOnline.Data;
using System.Runtime.CompilerServices;


namespace StockOnline.Droid.Common.Data
{
    public class ConnectFirebaseDB : IConnectFireBase
    {

        public ConnectFirebaseDB() { 
        
        
        }

        public string saveFoodDB(Entity.Food foodEntity) {
            string returnValue = "";

            try
            {
                HashMap aluminiInfo = new HashMap();
                aluminiInfo.Put("ID", foodEntity.ID);
                aluminiInfo.Put("Name", foodEntity.Name);
                aluminiInfo.Put("Location", foodEntity.Location);
                aluminiInfo.Put("ImageUrl", foodEntity.ImageUrl);
                aluminiInfo.Put("Details", foodEntity.Details);
                aluminiInfo.Put("CostPerUnit", foodEntity.CostPerUnit.ToString());
                aluminiInfo.Put("Quantity", foodEntity.Quantity);



                DatabaseReference newAluminRef = AppDataHelper.GetDatabase().GetReference("foodMini").Push();
                newAluminRef.SetValue(aluminiInfo);
                //this.Dismiss();
            }
            catch (Exception ee) {
                returnValue = "Error" + ee.Message;
            }
            return returnValue;
        }

        public string saveFoodDB(Models.Food foodEntity)
        {
            throw new NotImplementedException();
        }
    }

    
}