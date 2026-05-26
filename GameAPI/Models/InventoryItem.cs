namespace GameAPI.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ItemKey { get; set; } = string.Empty;   // np. "sword_001"
        public int Quantity { get; set; } = 1;
        public string? MetaData { get; set; }  // JSON z dodatkowymi danymi (level, stats)
        public DateTime ObtainedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
    }
}