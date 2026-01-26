using DeepAzureServer.Models.Enums;

namespace DeepAzureServer.Models.Entities
{
    public class Item : BaseAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemEffectType EffectType { get; set; }
        public int EffectPower { get; set; }
        public int? Price { get; set; }

        public ICollection<UserItem> UserItems = new List<UserItem>();
    }
}
