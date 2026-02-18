namespace DeepAzureServer.Models.Responses
{
    public class MonsterResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BaseHealth { get; set; }
        public int BaseStrength { get; set; }
        public int BaseDefense { get; set; }
        public int BaseMagic { get; set; }
        public int BaseResistance { get; set; }
        public int BaseSpeed { get; set; }
        public int? Price { get; set; }
        public ReferenceDto PrimaryElement { get; set; }
        public ReferenceDto? SecondaryElement { get; set; }
        public ReferenceDto? Ability { get; set; }

    }
}
