using WarehouseManagement.Models.Entities;

namespace WarehouseManagement.Services.Interfaces
{
    public interface IResourceService
    {
        Task<IEnumerable<Resource>> GetAllResourcesAsync();
        Task<Resource?> GetByNameAsync(string name);

        Task<Resource?> GetByIdAsync(int id);
        Task AddAsync(Resource entity);
        Task UpdateAsync(Resource entity);
        Task DeleteAsync(int id);

        Task<IEnumerable<Resource>> GetActiveResourcesAsync();
        IQueryable<Resource> GetAllQuery();
    }
}
