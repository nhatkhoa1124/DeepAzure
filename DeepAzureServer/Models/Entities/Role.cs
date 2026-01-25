namespace DeepAzureServer.Models.Entities
{
    public class Role : BaseAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users = new List<User>();
    }
}
