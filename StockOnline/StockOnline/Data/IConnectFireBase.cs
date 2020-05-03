using System;
using System.Collections.Generic;
using System.Text;
using StockOnline.Models;

namespace StockOnline.Data
{
    public interface IConnectFireBase
    {
        string saveFoodDB(Food foodEntity);
    }
}
