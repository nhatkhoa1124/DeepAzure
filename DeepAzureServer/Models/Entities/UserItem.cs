namespace DeepAzureServer.Models.Entities
{
    public class UserItem : BaseAuditable
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        public User? User { get; set; }
        public Item? Item { get; set; }
    }
}
