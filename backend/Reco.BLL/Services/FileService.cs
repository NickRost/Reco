using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage.Blob;
using Reco.DAL.Entities;
using Reco.DAL.Context;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Reco.BLL.Services
{
    public class FileService
    {
        private readonly RecoDbContext _context;

        public FileService(RecoDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveVideo(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var authorId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;
            if (authorId == null)
            {
                throw new Exception("Can not get user id from token");
            }

            Video newVideo = new Video()
            {
                IsPrivate = true,
                IsSaving = false, //Rename to IsSaved
                AuthorId = Convert.ToInt32(authorId),
                CreatedAt = DateTime.Now,
                Name = DateTime.Now.ToString("yyyy`-`MM`-`dd`_`HH`:`mm`:`ss"),
                Link = "",
            };

            await _context.AddAsync(newVideo);
            await _context.SaveChangesAsync();

            return newVideo.Id;
        }

        public async Task FinishLoadingFile(int id, string uri)
        {
            var video = await _context.Videos.FirstOrDefaultAsync(x => x.Id == id);
            if (video == null)
            {
                throw new Exception("Video with such identifier is not found.");
            }

            video.IsSaving = true; //Rename to IsSaved
            video.Link = uri;

            _context.Videos.Update(video);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> CheckAccessToFile(string token, int videoId)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var authorId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;
            if (authorId == null)
            {
                throw new Exception("Can not get user id from token");
            }

            var video = await _context.Videos.FirstOrDefaultAsync(x => x.Id == videoId);
            if (video == null)
            {
                throw new Exception("Video with such identifier is not found.");
            }
            if (video.AuthorId != Convert.ToInt32(authorId))
            {
                return false;
            }

            return true;
        }

    }
}
