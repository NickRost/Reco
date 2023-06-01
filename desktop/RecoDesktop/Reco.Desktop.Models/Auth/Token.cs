using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco.Desktop.Models.Auth
{
    public class Token
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public Token(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
