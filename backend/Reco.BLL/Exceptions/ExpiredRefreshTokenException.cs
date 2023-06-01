using System;

namespace Reco.BLL.Exceptions
{
    public class ExpiredRefreshTokenException : Exception
    {
        public ExpiredRefreshTokenException() 
            : base("Refresh token expired.")
        {
            
        }
    }
}
