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
        
        public User? User { get; set; }
        public Match? Match { get; set; }
    }
}
