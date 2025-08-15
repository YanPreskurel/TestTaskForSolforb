namespace WarehouseManagement.Models.DTOs
{
    public class UnitUpdateDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
