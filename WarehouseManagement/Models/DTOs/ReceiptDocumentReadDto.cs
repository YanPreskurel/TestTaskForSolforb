namespace WarehouseManagement.Models.DTOs
{
    public class ReceiptDocumentReadDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<ReceiptResourceReadDto> Resources { get; set; } = new();
    }
}
