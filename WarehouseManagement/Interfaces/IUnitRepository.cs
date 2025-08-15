using WarehouseManagement.Models.Entities;

namespace WarehouseManagement.Interfaces
{
    public interface IUnitRepository : IBaseRepository<Unit>
    {
        Task<Unit?> GetByNameAsync(string name); // Для проверки уникальности имени

        IQueryable<Unit> GetAllQuery();
    }
}
