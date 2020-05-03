using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace StockOnline.Common
{
    interface IFileIO
    {

        Stream CreateFile(string path);
        string GetMyDocumentsPath();
    }
}
