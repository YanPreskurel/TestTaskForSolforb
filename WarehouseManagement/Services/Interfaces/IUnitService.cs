using WarehouseManagement.Models.Entities;

namespace WarehouseManagement.Services.Interfaces
{
    public interface IUnitService
    {
        Task<IEnumerable<Unit>> GetAllUnitsAsync();
        Task<Unit?> GetByNameAsync(string name);
        Task<IEnumerable<Unit>> GetActiveUnitsAsync();

        Task<Unit?> GetByIdAsync(int id);
        Task AddAsync(Unit entity);
        Task UpdateAsync(Unit entity);
        Task DeleteAsync(int id);

        IQueryable<Unit> GetAllQuery();
    }
}
