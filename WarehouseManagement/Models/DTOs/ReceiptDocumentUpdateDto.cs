namespace WarehouseManagement.Models.DTOs
{
    public class ReceiptDocumentUpdateDto
    {
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<ReceiptResourceUpdateDto> Resources { get; set; } = new();
    }
}
