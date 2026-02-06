namespace DeepAzureServer.Models.Entities
{
    public class Element : BaseAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Technique> Techniques { get; set; } = new List<Technique>();
    }
}
