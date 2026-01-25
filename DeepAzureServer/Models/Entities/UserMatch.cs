using System.Text.Json;

namespace DeepAzureServer.Models.Entities
{
    public class UserMatch : BaseAuditable
    {
        public int UserId { get; set; }
        public int MatchId { get; set; }
        public JsonDocument TeamSnapshot { get; set; }
        public int EloChange { get; set; }
        public string Outcome { get; set; }
        public int GoldEarned { get; set; }

        public ICollection<User> Users = new List<User>();
        public ICollection<Match> Matches = new List<Match>();
    }
}
