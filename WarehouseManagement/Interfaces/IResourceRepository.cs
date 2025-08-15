using WarehouseManagement.Models.Entities;

namespace WarehouseManagement.Interfaces
{
    public interface IResourceRepository : IBaseRepository<Resource>
    {
        Task<Resource?> GetByNameAsync(string name); // Для проверки уникальности имени

        IQueryable<Resource> GetAllQuery();
    }
}
