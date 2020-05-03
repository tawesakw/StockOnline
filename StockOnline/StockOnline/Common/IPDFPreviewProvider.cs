using System;
using System.Collections.Generic;
using System.Text;

namespace StockOnline.Common
{
    interface IPDFPreviewProvider
    {

        void TriggerPreview(string path);

    }
}
