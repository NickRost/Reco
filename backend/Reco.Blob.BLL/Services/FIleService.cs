using Reco.Blob.BLL.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Reco.Blob.BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IAzureBlobConnectionFactory _azureBlobConnectionFactory;
        private readonly IRequestService _requestService;

        public FileService(IAzureBlobConnectionFactory azureBlobConnectionFactory, IRequestService requestService)
        {
            _azureBlobConnectionFactory = azureBlobConnectionFactory;
            _requestService = requestService;
        }

        public async Task<string> UploadAsync(Stream files, string token, int id)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
            var blob = blobContainer.GetBlockBlobReference(id + ".mp4");

            await using var fileStream = files;
            await blob.UploadFromStreamAsync(fileStream);

            var uri = blob.Uri.AbsoluteUri;

            return await _requestService.SendFinishRequest(Convert.ToInt32(id), uri);
        }

        public async Task<bool> DeleteAsync(int id, string token)
        {
            if (token is null)
            {
                return false;
            }

            var deleteResult = await _requestService.SendDeleteRequest(id, token.Replace("Bearer ", ""));

            if (deleteResult)
            {
                var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

                var blob = blobContainer.GetBlockBlobReference(id.ToString() + ".mp4");

                var blobResult = await blob.DeleteIfExistsAsync();

                return blobResult;
            }
            else
            {
                return false;
            }
        }

        public async Task<(Stream response, int? errorCode)> DownloadAsync(int id, string token)
        {
            if (await _requestService.SendGetRequest(id, token))
            {
                var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

                var blob = blobContainer.GetBlockBlobReference(id.ToString());

                return (await blob.OpenReadAsync(), null);
            }
            else
            {
                return (null, 403);
            }
        }

        public async Task<string> GetUrlAsync(int id, string token)
        {
            //var res = await _requestService.SendGetRequest(id, token.Replace("Bearer ", ""));
            //if (!res)
            //return null;

            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

            var blob = blobContainer.GetBlockBlobReference(id.ToString() + ".mp4");

            return blob.Uri.AbsoluteUri;
        }
    }
}
