using Microsoft.AspNetCore.WebUtilities;
using Reco.Desktop.Models.Auth;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Reco.Desktop.Logic
{
    public class AuthorizationService
    {
        readonly private DefaultBrowser _browser;
        readonly private string _redirect_url;
        readonly Dictionary<string, string> _authRequestParams;

        public AuthorizationService(string endpoint)
        {
            _redirect_url = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";
            _authRequestParams = new Dictionary<string, string>
            {
                { "redirect_url", _redirect_url }
            };

            _browser = new DefaultBrowser(GetAuthRequestUrl(endpoint), _redirect_url);          
        }

        public async Task<Token> Authorize()
        {
            string result = await _browser.InvokeAsync();
            return ParseRawAuthResult(result);
        }

        private string GetAuthRequestUrl(string endpoint)
        {
            return QueryHelpers.AddQueryString(endpoint, _authRequestParams);
        }

        private static Token ParseRawAuthResult(string result)
        {            
            var queryParams = QueryHelpers.ParseNullableQuery(result);
            return new Token(queryParams["access_token"], string.Empty );               
        }

        private static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
    }
}
