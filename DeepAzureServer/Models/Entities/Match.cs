using System.Text.Json;

namespace DeepAzureServer.Models.Entities
{
    public class Match : BaseAuditable
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string MatchType { get; set; }
        public DateTime? EndedAt { get; set; }
        public string? ReplayLog { get; set; }

        public ICollection<UserMatch> UserMatches { get; set; } = new List<UserMatch>();
    }
}
