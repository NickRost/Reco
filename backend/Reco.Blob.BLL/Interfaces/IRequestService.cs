using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Reco.Blob.BLL.Interfaces
{
    public interface IRequestService
    {
        Task<string> SendSaveRequest(string token);
        Task<bool> SendGetRequest(int id, string token);
        Task<string> SendFinishRequest(int videoId, string uri);
        Task<bool> SendDeleteRequest(int videoId, string token);
    }
}
