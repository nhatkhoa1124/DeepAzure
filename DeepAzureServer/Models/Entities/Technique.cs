using DeepAzureServer.Models.Enums;

namespace DeepAzureServer.Models.Entities
{
    public class Technique : BaseAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StrengthDamage { get; set; }
        public int MagicDamage { get; set; }
        public TechniqueTarget Target { get; set; }
        public TechniqueStatType StatType { get; set; }
        public int StatAmout { get; set; }
        public int ElementId { get; set; }
        public int? StatusEffectId { get; set; }

        public Element? Element { get; set; }
        public StatusEffect? StatusEffect { get; set; }
    }
}
