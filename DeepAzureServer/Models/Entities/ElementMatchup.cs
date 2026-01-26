namespace DeepAzureServer.Models.Entities
{
    public class ElementMatchup : BaseAuditable
    {
        public int Id { get; set; }
        public float Multiplier { get; set; }
        public int AttackerId { get; set; }
        public int DefenderId { get; set; }

        public Element? Attacker { get; set; }
        public Element? Defender { get; set; }
    }
}
