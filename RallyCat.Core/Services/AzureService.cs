using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace RallyCat.Core.Services
{
    public class AzureService
    {
        private RallyBackgroundData _rallyBackgroundData;

        public AzureService( RallyBackgroundData backgroundData)
        {
            _rallyBackgroundData = backgroundData;
        }
        public string Upload(Image img, string fileName)
        {
            
            var azureName = _rallyBackgroundData.RallyGlobalConfiguration.AzureBlobName;
            var azureToken = _rallyBackgroundData.RallyGlobalConfiguration.AzureToken;
            var azureContainerRef = _rallyBackgroundData.RallyGlobalConfiguration.AzureBlobContainerRef;
            var kanbanImageFormat = _rallyBackgroundData.RallyGlobalConfiguration.KanbanImageFormat;
            StorageCredentials sc = new StorageCredentials(azureName, azureToken);
            CloudStorageAccount acc = new CloudStorageAccount(sc, false);
            CloudBlobClient bc = acc.CreateCloudBlobClient();
            CloudBlobContainer bcon = bc.GetContainerReference(azureContainerRef);
            CloudBlockBlob cbb = bcon.GetBlockBlobReference(fileName + DateTime.Now.ToString("o").Replace(":", "_") + ".jpg");
            cbb.Properties.ContentType = kanbanImageFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                byte[] b = ms.GetBuffer();
                cbb.UploadFromByteArray(b, 0, b.Count());
            }
            var path = cbb.Uri.AbsoluteUri;
            return path;
        }
    }
}
