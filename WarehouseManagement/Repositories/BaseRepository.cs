using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Data;
using WarehouseManagement.Interfaces;

namespace WarehouseManagement.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context) 
        { 
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsunc(id);

            if(entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsunc(int id)
        {
            return await _dbSet.FindAsync(id) 
                ?? throw new KeyNotFoundException($"Resource with id = {id} not found");
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
