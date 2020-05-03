using System.IO;

namespace StockOnline
{
    public interface IPrintService
    {
        void Print(Stream inputStream, string fileName);

    }
}
