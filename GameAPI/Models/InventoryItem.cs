namespace GameAPI.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ItemKey { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public string? MetaData { get; set; }  // JSON with item-specific data (e.g., durability, enchantments)
        public DateTime ObtainedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
    }
}