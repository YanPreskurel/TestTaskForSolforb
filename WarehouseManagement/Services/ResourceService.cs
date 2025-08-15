using WarehouseManagement.Interfaces;
using WarehouseManagement.Models.Entities;
using WarehouseManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WarehouseManagement.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _repository;

        public ResourceService(IResourceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Resource>> GetAllResourcesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Resource?> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<Resource?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsunc(id);
        }

        public async Task AddAsync(Resource entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(Resource entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Resource>> GetActiveResourcesAsync()
        {
            return await Task.FromResult(_repository.GetAllQuery().Where(u => !u.IsActive));
        }

        public IQueryable<Resource> GetAllQuery()
        {
            return _repository.GetAllQuery();
        }
    }
}
