using System;

namespace Reco.BLL.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string email)
         : base($"User with email {email} not exists.")
        { }
    }
}
