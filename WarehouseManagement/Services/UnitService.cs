using WarehouseManagement.Interfaces;
using WarehouseManagement.Models.Entities;
using WarehouseManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WarehouseManagement.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _repository;

        public UnitService(IUnitRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Unit>> GetAllUnitsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Unit?> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<IEnumerable<Unit>> GetActiveUnitsAsync()
        {
            return await Task.FromResult(_repository.GetAllQuery().Where(u => !u.IsActive));
        }
        public async Task<Unit?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsunc(id);
        }
        public async Task AddAsync(Unit entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(Unit entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public IQueryable<Unit> GetAllQuery()
        {
            return _repository.GetAllQuery();
        }
    }
}
