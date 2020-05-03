using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace StockOnline.Data
{
    public interface ISLite
    {
        SQLiteConnection GetConnection();
    }
}
