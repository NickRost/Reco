namespace Reco.DAL.Entities
{
    public class AccessForUnregisteredUsers
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int VideoId { get; set; }
        public Video Video { get; set; }
    }
}