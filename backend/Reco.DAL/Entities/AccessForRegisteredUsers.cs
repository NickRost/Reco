namespace Reco.DAL.Entities
{
    public class AccessForRegisteredUsers
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int VideoId { get; set; }
        public Video Video { get; set; }
    }
}