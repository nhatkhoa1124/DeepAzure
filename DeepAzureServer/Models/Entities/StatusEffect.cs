using DeepAzureServer.Models.Enums;

namespace DeepAzureServer.Models.Entities
{
    public class StatusEffect : BaseAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusEffectType Type { get; set; }

        public ICollection<Technique> Techniques = new List<Technique>();
    }
}
