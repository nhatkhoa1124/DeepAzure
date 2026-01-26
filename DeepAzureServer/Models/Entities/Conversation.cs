namespace DeepAzureServer.Models.Entities
{
    public class Conversation : BaseAuditable
    {
        public int Id { get; set; }
        public DateTime LastMessageAt { get; set; }
        public int User1Id { get; set; }
        public int User2Id { get; set; }

        public User? User1 { get; set; }
        public User? User2 { get; set; }
        public ICollection<Message> Messages = new List<Message>();
    }
}
