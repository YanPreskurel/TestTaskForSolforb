using WarehouseManagement.Data;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WarehouseManagement.Repositories
{
    public class UnitRepository : BaseRepository<Unit>, IUnitRepository
    {
        public UnitRepository(AppDbContext context) : base(context) { }

        public IQueryable<Unit> GetAllQuery()
        {
            return _context.Set<Unit>().AsQueryable();
        }

        public async Task<Unit?> GetByNameAsync(string name)
        {
            return await _context.Units.FirstOrDefaultAsync(u => u.Name == name);
        }
    }
}
