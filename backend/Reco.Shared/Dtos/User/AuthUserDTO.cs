using Reco.Shared.Dtos.Auth;

namespace Reco.Shared.Dtos.User
{
    public class AuthUserDTO
    {
        public UserDTO User { get; set; }
        public TokenDTO Token { get; set; }
    }
}
