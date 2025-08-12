namespace WarehouseManagement.Models
{
    public class ReceiptResource
    {
        public int Id { get; set; }

        public int ReceiptDocumentId { get; set; }
        public ReceiptDocument? ReceiptDocument { get; set; }

        public int ResourceId { get; set; }
        public Resource? Resource { get; set; }

        public int UnitId { get; set; }
        public Unit? Unit { get; set; }

        public decimal Quantity { get; set; }
    }
}

