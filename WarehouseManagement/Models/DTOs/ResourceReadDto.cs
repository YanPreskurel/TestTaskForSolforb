namespace WarehouseManagement.Models.DTOs
{
    public class ResourceReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
