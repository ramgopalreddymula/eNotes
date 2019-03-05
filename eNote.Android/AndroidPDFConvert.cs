using System;
using System.IO;
using Android.Graphics.Pdf;
using Android.Webkit;
using eNote.Droid;
using eNote.Services;
using Java.IO;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidPDFConvert))]
namespace eNote.Droid
{
    public class AndroidPDFConvert : IPDFConvert
    {


        WebView webpage;
        public string SafeHTMLToPDF(string html, string filename)
        {

            var dir = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/eNotes/");
            var file=Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename+".pdf");
            //var file = new Java.IO.File(dir + "/" + filename + ".pdf");

            if (!dir.Exists())
                dir.Mkdirs();


            int x = 0;
           /* while (file.Exists())
            {
                x++;
                file = new Java.IO.File(dir + "/" + filename + "( " + x + " )" + ".pdf");
            }*/

            if (webpage == null)
                webpage = new Android.Webkit.WebView(MainActivity.mContext);

            int width = 2102;
            int height = 2973;

            webpage.Layout(0, 0, width, height);
            webpage.LoadDataWithBaseURL("", html, "text/html", "UTF-8", null);
            webpage.SetWebViewClient(new WebViewCallBack(file.ToString()));

            return file.ToString();
        }
    }

        class WebViewCallBack : WebViewClient
        {

            string fileNameWithPath = null;

            public WebViewCallBack(string path)
            {
                this.fileNameWithPath = path;
            }


            public override void OnPageFinished(Android.Webkit.WebView myWebview, string url)
            {
                PdfDocument document = new PdfDocument();
                PdfDocument.Page page = document.StartPage(new PdfDocument.PageInfo.Builder(2120, 3000, 1).Create());

                myWebview.Draw(page.Canvas);
                document.FinishPage(page);
                Stream filestream = new MemoryStream();
                FileOutputStream fos = new Java.IO.FileOutputStream(fileNameWithPath, false); ;
                try
                {
                    document.WriteTo(filestream);
                    fos.Write(((MemoryStream)filestream).ToArray(), 0, (int)filestream.Length);
                    fos.Close();
                }
                catch
                {
                }
            }
        }
    
}
