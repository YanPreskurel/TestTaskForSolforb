using WarehouseManagement.Data;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WarehouseManagement.Repositories
{
    public class ResourceRepository : BaseRepository<Resource>, IResourceRepository
    {
        public ResourceRepository(AppDbContext context) : base(context) { }

        public IQueryable<Resource> GetAllQuery()
        {
            return _context.Set<Resource>().AsQueryable();
        }

        public async Task<Resource?> GetByNameAsync(string name)
        {
            return await _context.Resources.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
