namespace WarehouseManagement.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Наименование
        public bool IsActive { get; set; } = true;
    }
}
