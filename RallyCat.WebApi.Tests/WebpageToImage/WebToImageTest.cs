using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WatiN.Core;

namespace RallyCat.WebApi.Tests.WebpageToImage
{
    [TestClass]
    public class WebToImageTest
    {
        [TestMethod]
        public void WebPageToImageTest()
        {
            //Capture(null, null);
            using (var ie = new IE("http://watin.org/"))
            {
                ie.CaptureWebPageToFile(@"c:\temp\g1.jpg");
            }
        }
        
        protected void Capture(object sender, EventArgs e)
        {
            string url = "http://www.aspsnippets.com/Articles/Capture-Screenshot-Snapshot-Image-of-Website-Web-Page-in-ASPNet-using-C-and-VBNet.aspx";
            Thread thread = new Thread(delegate()
            {
                using (WebBrowser browser = new WebBrowser())
                {
                    browser.ScrollBarsEnabled = false;
                    browser.AllowNavigation = true;
                    browser.Navigate(url);
                    browser.Width = 1024; 
                    browser.Height = 768;
                    browser.DocumentCompleted += DocumentCompleted;
                    while (browser.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Application.DoEvents();
                    }
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        private void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;
            int w = browser.Document.Body.ScrollRectangle.Width;
            int h = browser.Document.Body.ScrollRectangle.Height;
            browser.ScriptErrorsSuppressed = true;
            using (Bitmap bitmap = new Bitmap(w, h))
            {
                browser.DrawToBitmap(bitmap, new Rectangle(0, 0, browser.Width, browser.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] bytes = stream.ToArray();
                    using (FileStream sw = new FileStream(@"c:\temp\1.png",FileMode.OpenOrCreate))
                    {
                        using (BinaryWriter bw = new BinaryWriter(sw))
                        {
                            bw.Write(bytes);
                        }
                    }
                    //imgScreenShot.Visible = true;
                    //imgScreenShot.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
                }
            }
        }
    }


}
