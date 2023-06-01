using System;

namespace Reco.BLL.Exceptions
{
    public class VerifyGoogleTokenException : Exception
    {
        public VerifyGoogleTokenException()
            : base($"Invalid google token")
        {

        }
    }
}
