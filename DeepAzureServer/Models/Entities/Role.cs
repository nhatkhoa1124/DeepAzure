namespace DeepAzureServer.Models.Entities
{
    public class Role : BaseAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
