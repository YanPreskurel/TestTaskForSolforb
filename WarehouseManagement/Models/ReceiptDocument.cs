namespace WarehouseManagement.Models
{
    public class ReceiptDocument
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTime Date { get; set; }

        public List<ReceiptResource> ReceiptResources { get; set; } = new();
    }
}
