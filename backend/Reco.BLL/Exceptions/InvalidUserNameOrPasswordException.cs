using System;

namespace Reco.BLL.Exceptions
{
    public sealed class InvalidUserNameOrPasswordException : Exception
    {
        public InvalidUserNameOrPasswordException() 
            : base("Invalid username or password.") 
        {

        }
    }
}
