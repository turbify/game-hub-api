namespace GameAPI.DTOs
{
    public class AddItemRequest
    {
        public string ItemKey { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public string? MetaData { get; set; }
    }

    public class UpdateItemRequest
    {
        public int Quantity { get; set; }
        public string? MetaData { get; set; }
    }

    public class InventoryItemResponse
    {
        public int Id { get; set; }
        public string ItemKey { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? MetaData { get; set; }
        public DateTime ObtainedAt { get; set; }
    }
}