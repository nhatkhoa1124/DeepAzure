namespace DeepAzureServer.Models.Entities
{
    public class UserBadge : BaseAuditable
    {
        public int UserId { get; set; }
        public int BadgeId { get; set; }
        public DateTime UnlockedAt { get; set; }

        public Badge? Badge { get; set; }
        public User? User { get; set; }
    }
}
