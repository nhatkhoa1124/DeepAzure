using System.Text.Json;

namespace DeepAzureServer.Models.Entities
{
    public class Ability : BaseAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogicKey { get; set; }
        public JsonDocument LogicData { get; set; }
    }
}
