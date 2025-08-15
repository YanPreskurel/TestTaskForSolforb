namespace WarehouseManagement.Models.DTOs
{
    public class ReceiptResourceUpdateDto
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }
    }
}
