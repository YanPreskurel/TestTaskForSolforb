namespace WarehouseManagement.Models.DTOs
{
    public class ReceiptResourceReadDto
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string ResourceName { get; set; } = string.Empty;
        public int UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
    }
}
