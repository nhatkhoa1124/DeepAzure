namespace DeepAzureServer.Models.Entities
{
    public class Conversation : BaseAuditable
    {
        public int Id { get; set; }
        public DateTime LastMessageAt { get; set; }
        public long User1Id { get; set; }
        public long User2Id { get; set; }

        public User? User1 { get; set; }
        public User? User2 { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
