using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ConsoleApp33
{

    public class AzureStorage : IAzureStorage
    {
        private readonly CloudBlobContainer blobContainer;

        public AzureStorage(IOptions<AzureConfiguration> options)
        {
            var account = CloudStorageAccount.Parse(options.Value.ConnectionString);
            var blobClient = account.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference("images");
            blobContainer.CreateIfNotExistsAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public Task Put(byte[] image, string name)
        {
            var blob = blobContainer.GetBlockBlobReference($"{name}.png");
            return blob.UploadFromByteArrayAsync(image, 0, image.Length);
        }
    }
}
