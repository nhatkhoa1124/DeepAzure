using Microsoft.AspNetCore.Identity;

namespace DeepAzureServer.Models.Entities
{
    public class User : IdentityUser<long>
    {
        public string? DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? AvatarPublicId { get; set; }
        public int? EloRating { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
        public ICollection<UserMatch> UserMatches { get; set; } = new List<UserMatch>();
        public ICollection<UserItem> UserItems { get; set; } = new List<UserItem>();
        public ICollection<UserMonster> UserMonsters { get; set; } = new List<UserMonster>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
