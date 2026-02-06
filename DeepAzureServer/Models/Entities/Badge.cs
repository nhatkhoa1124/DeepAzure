namespace DeepAzureServer.Models.Entities
{
    public class Badge : BaseAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
    }
}
