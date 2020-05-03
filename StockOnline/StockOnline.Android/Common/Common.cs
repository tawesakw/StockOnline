using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Java.IO;
namespace StockOnline.Droid.Common
{
    public class Common
    {
        public static string GetAppPath(Context context) {
            File dir = new File(Android.OS.Environment.ExternalStorageDirectory
                + File.Separator
                + context.GetString(Resource.String.app_name)
                + File.Separator);
            if (!dir.Exists()) {
                dir.Mkdir();

            }

            return dir.Path + File.Separator;
        }

        public static async Task WriteFileToStorageAsync(Context context, String fileName) {

            AssetManager assets = context.Assets;
            if (new File(GetFilePath(context, fileName)).Exists()) {
                return;
            
            }
            try
            {

                var input = assets.Open(fileName);
                var output = new FileOutputStream(GetFilePath(context, fileName));

                byte[] buffer = new byte[8 * 1024];
                int length;
                while ((length = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, length);
                }
            }
            catch (FileNotFoundException ex)
            {
                ex.PrintStackTrace();

            }
            catch (IOException exx) {
                exx.PrintStackTrace();
            }
        
        
        }

        private static String GetFilePath(Context context, string fileName)
        {
            return context.FilesDir + File.Separator + fileName;
        }
    }
}