namespace WarehouseManagement.Models.DTOs
{
    public class ResourceUpdateDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
