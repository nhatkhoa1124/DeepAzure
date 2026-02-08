namespace DeepAzureServer.Models.Entities
{
    public class Message : BaseAuditable
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int ConversationId { get; set; }
        public long SenderId { get; set; }

        public Conversation? Conversation { get; set; }
        public User? Sender { get; set; }
    }
}
