using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Reco.Desktop.Logic
{
    public class DefaultBrowser
    {
        private readonly string _url;
        private readonly string _redirectUrl;
        public DefaultBrowser(string url, string redirectUrl)
        {
            _url = url;
            _redirectUrl = redirectUrl;
        }

        public async Task<string> InvokeAsync(CancellationToken cancellationToken = default)
        {
            using var listener = new LoopbackHttpListener(_redirectUrl);
            Open(_url);

            try
            {
                var result = await listener.WaitForCallbackAsync();
                if (string.IsNullOrWhiteSpace(result))
                {
                    return "Empty response.";
                }

                return result;
            }
            catch (TaskCanceledException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public static void Open(string url)
        {
            try 
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
