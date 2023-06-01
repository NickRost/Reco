

using Microsoft.AspNetCore.Mvc;
using Reco.BLL.Exceptions;
using System.Linq;

namespace Reco.API.Extensions
{

    public static class ControllerExtentions
    {
        public static int GetUserIdFromToken(this ControllerBase controller)
        {
            var claimsUserId = controller.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

            if (string.IsNullOrEmpty(claimsUserId))
            {
                throw new InvalidTokenException("access");
            }

            return int.Parse(claimsUserId);
        }
    }
}
