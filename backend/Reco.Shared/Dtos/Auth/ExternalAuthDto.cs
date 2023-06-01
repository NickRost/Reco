namespace Reco.Shared.Dtos.Auth
{
    public class ExternalAuthDto
    {
        public string Provider { get; set; }
        public string IdToken { get; set; }
        public string WorkspaceName { get; set; } = "My Workspace";
    }
}
