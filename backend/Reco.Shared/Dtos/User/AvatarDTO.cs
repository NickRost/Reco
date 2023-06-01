using Newtonsoft.Json;

namespace Reco.Shared.Dtos.User
{
    public class AvatarDTO
    {
        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }
    }
}
