using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using StockOnline.Data;
using System.IO;
using Xamarin.Forms;
using StockOnline.iOS.Data;

[assembly: Dependency(typeof(SQLLite_IOS))]
namespace StockOnline.iOS.Data
{
    public class SQLLite_IOS : ISLite
    {

        public SQLLite_IOS() { }

        public SQLite.SQLiteConnection GetConnection()
        {
            var fileName = "Pos.db3";
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            var connection = new SQLite.SQLiteConnection(path);
            return connection;

        }
    }
}