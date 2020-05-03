using System;
using System.Collections.Generic;
using System.Text;

namespace StockOnline.Common
{
    public interface INetworkConnection
    {
        bool IsConnected { get; }
        void CheckNetworkConnection();
    }
}
