using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Models.Entities
{
    public class ReceiptDocument
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите номер")]
        [StringLength(50)]
        public string? Number { get; set; }
        public DateTime Date { get; set; }

        public List<ReceiptResource> ReceiptResources { get; set; } = new();
    }
}
