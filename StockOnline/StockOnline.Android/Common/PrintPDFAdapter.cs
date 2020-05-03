﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Print;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StockOnline.Droid.Common;
using Java.IO;

namespace StockOnline.Droid.Common
{
    public class PrintPDFAdapter : PrintDocumentAdapter
    {
        Context context;
        string path;
        public PrintPDFAdapter(Context context, string path) {
            this.context = context;
            this.path = path;
        
        }
        /*
        public override void OnLayout (PrintAttributes oldAttributes, PrintAttributes newAttributes, CancellationSignal cancellation) {
            if (cancellation.IsCanceled)
            {
                  AsyncCallback.
            }
            else { 
            
            }
        
        }
        */

        public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes, CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
        {
            if (cancellationSignal.IsCanceled)
            {
                callback.OnLayoutCancelled();    
            }
            else {
                PrintDocumentInfo.Builder builder = new PrintDocumentInfo.Builder("Order.pdf");
                builder.SetContentType(PrintContentType.Document)
                       .SetPageCount(PrintDocumentInfo.PageCountUnknown)
                       .Build();

                callback.OnLayoutFinished(builder.Build(), !newAttributes.Equals(oldAttributes));
            }   
        }

        public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination, CancellationSignal cancellationSignal, WriteResultCallback callback)
        {
            InputStream input = null;
            OutputStream output = null;
            try
            {
                File file = new File(path);
                input = new FileInputStream(file);
                output = new FileOutputStream(destination.FileDescriptor);

                byte[] buff = new byte[8 * 1024];
                int length;
                while ((length = input.Read(buff)) >= 0 && !cancellationSignal.IsCanceled)
                {
                    output.Write(buff, 0, length);
                }
                if (cancellationSignal.IsCanceled)
                {
                    callback.OnWriteCancelled();
                }
                else
                {
                    callback.OnWriteFinished(new PageRange[] { PageRange.AllPages });

                }




            }
            catch (Exception e)
            {
                callback.OnWriteFailed(e.Message);
            }
            finally {
                try
                {
                    input.Close();
                    output.Close();
                }
                catch (IOException e) { 
                    
                }
            }


        }

    }



}