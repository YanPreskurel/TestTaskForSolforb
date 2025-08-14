using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Models.Entities
{
    public class Unit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите наименование")]
        [StringLength(50)]
        public string? Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
