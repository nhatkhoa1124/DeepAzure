namespace DeepAzureServer.Models.Entities
{
    public class Monster : BaseAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BaseHealth { get; set; }
        public int GrowthHealth { get; set; }
        public int BaseStrength { get; set; }
        public int GrowthStrength { get; set; }
        public int BaseDefense { get; set; }
        public int GrowthDefense { get; set; }
        public int BaseMagic { get; set; }
        public int GrowthMagic { get; set; }
        public int BaseResistance { get; set; }
        public int GrowthResistance { get; set; }
        public int BaseSpeed { get; set; }
        public int GrowthSpeed { get; set; }
        public int Price { get; set; }
        public int PrimaryElementId { get; set; }
        public int SecondaryElementId { get; set; }
        public int AbilityId { get; set; }

        public Element? PrimaryElement { get; set; }
        public Element? SecondaryElement { get; set; }
        public Ability? Ability { get; set; }
        public ICollection<UserMonster> UserMonsters = new List<UserMonster>();
    }
}
