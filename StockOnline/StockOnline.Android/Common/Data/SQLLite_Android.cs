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
using StockOnline.Data;
using System.IO;
using Xamarin.Forms;
using StockOnline.Droid.Data;
using StockOnline.Models;

[assembly: Dependency(typeof(SQLLite_Android))]


namespace StockOnline.Droid.Data
{
    public class SQLLite_Android : ISLite
    {
        public SQLLite_Android() { }



        private static SQLite.SQLiteConnection connection;
        private static string databaseFilePath;

        public SQLite.SQLiteConnection GetConnection()
        {
           
            var sqlLiteFileName = "Pos.db3";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
           
            
            
            var path = Path.Combine(documentPath, sqlLiteFileName);
            var conn = new SQLite.SQLiteConnection(path);


            return conn;

        }

        public static void CreateDatabaseAndTables()
        {
            if (File.Exists(databaseFilePath))
                return;

            using (connection)
            {
                connection.CreateTable<Users>();
            }
        }

    }
}