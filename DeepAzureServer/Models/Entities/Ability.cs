using System.Text.Json;

namespace DeepAzureServer.Models.Entities
{
    public class Ability : BaseAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogicKey { get; set; }
        public string LogicData { get; set; } // This is JSON

        public ICollection<Monster> Monsters { get; set; } = new List<Monster>();
    }
}
