namespace Reco.BlobAPI.Dtos
{
    public class FileDto
    {
        public FileDto(string url)
        {
            Url = url;
        }
        public string Url { get; set; }
    }
}
