namespace DeepAzureServer.Models.Entities
{
    public class User : BaseAuditable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string? DisplayName { get; set; }
        public string Email { get; set; }
        public DateTime LastActive { get; set; }
        public string? AvatarUrl { get; set; }
        public string? AvatarPublicId { get; set; }
        public int? EloRating { get; set; }
        public int RoleId { get; set; }

        public Role? Role { get; set; }
        public ICollection<UserBadge> UserBadges = new List<UserBadge>();
        public ICollection<UserMatch> UserMatches = new List<UserMatch>();
        public ICollection<UserItem> UserItems = new List<UserItem>();
        public ICollection<UserMonster> UserMonsters = new List<UserMonster>();
        public ICollection<Message> Messages = new List<Message>();
    }
}
