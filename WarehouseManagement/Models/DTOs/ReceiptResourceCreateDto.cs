namespace WarehouseManagement.Models.DTOs
{
    public class ReceiptResourceCreateDto
    {
        public int ResourceId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }
    }
}
