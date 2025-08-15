using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Models.Entities
{
    public class ReceiptResource
    {
        public int Id { get; set; }

        [Required]
        public int ReceiptDocumentId { get; set; }
        public ReceiptDocument? ReceiptDocument { get; set; }

        [Required]
        public int ResourceId { get; set; }

        [Required]
        public Resource? Resource { get; set; }

        [Required]
        public int UnitId { get; set; }
        public Unit? Unit { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Quantity { get; set; }
    }
}

