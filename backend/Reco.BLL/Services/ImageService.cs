using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Reco.BLL.Services.Abstract;
using Reco.Shared.Dtos.User;
using Reco.DAL.Context;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reco.BLL.Services
{
    public class ImageService : BaseService
    {
        public ImageService(RecoDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<string> UploadToGyazo(IFormFile avatar, string gyazoKey)
        {
            using (var client = new HttpClient())
            {
                var url = "https://upload.gyazo.com/api/upload?access_token=" + gyazoKey;

                using (var memoryStream = new MemoryStream())
                {
                    await avatar.CopyToAsync(memoryStream);
                    var avatarImage = memoryStream.ToArray();

                    ByteArrayContent byteContent = new ByteArrayContent(avatarImage);
                    var multipartContent = new MultipartFormDataContent();
                    multipartContent.Add(byteContent, "imagedata", "imagedata");

                    var response = await client.PostAsync(url, multipartContent);
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    var json = JsonConvert.DeserializeObject<AvatarDTO>(stringResponse);
                    return json.ThumbUrl;
                }
            }
        }
    }
}
