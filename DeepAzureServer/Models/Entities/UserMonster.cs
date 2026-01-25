namespace DeepAzureServer.Models.Entities
{
    public class UserMonster : BaseAuditable
    {
        public int Id { get; set; }
        public string? Nickname { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int TeamSlot { get; set; }
        public int MonsterId { get; set; }
        public int OwnerId { get; set; }
        public int Move1Id { get; set; }
        public int? Move2Id { get; set; }
        public int? Move3Id { get; set; }
        public int? Move4Id { get; set; }

        public Monster? Monster { get; set; }
        public User? Owner { get; set; }
        public Technique? Move1 { get; set; }
        public Technique? Move2 { get; set; }
        public Technique? Move3 { get; set; }
        public Technique? Move4 { get; set; }
    }
}
