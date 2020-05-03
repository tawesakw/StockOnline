using Android.Content;
using Android.Print;
using StockOnline.Droid;
using System.IO;
using Xamarin.Forms;

//Register the Android implementation of the interface with DependencyService
[assembly: Dependency(typeof(PrintService))]

namespace StockOnline.Droid
{
    class PrintService : IPrintService
    {
        public void Print(Stream inputStream, string fileName)
        {
            if (inputStream.CanSeek)
                //Reset the position of PDF document stream to be printed
                inputStream.Position = 0;
            string createdFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);
            using (var dest = System.IO.File.OpenWrite(createdFilePath))
                inputStream.CopyTo(dest);
            string filePath = createdFilePath;
            PrintManager printManager = (PrintManager)Forms.Context.GetSystemService(Context.PrintService);
            PrintDocumentAdapter pda = new CustomPrintDocumentAdapter(filePath);
            //Print with null PrintAttributes
            printManager.Print(fileName, pda, null);
        }
    }
}