namespace WarehouseManagement.Models.DTOs
{
    public class ReceiptDocumentCreateDto
    {
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<ReceiptResourceCreateDto> Resources { get; set; } = new();
    }
}
