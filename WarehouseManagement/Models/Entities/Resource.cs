using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Models.Entities
{
    public class Resource
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите наименование")]
        [StringLength(100)]
        public string? Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
