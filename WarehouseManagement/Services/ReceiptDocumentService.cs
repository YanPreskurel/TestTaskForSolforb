using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models.Entities;
using WarehouseManagement.Services.Interfaces;

namespace WarehouseManagement.Services
{
    public class ReceiptDocumentService : IReceiptDocumentService
    {
        private readonly IReceiptDocumentRepository _repository;

        public ReceiptDocumentService(IReceiptDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ReceiptDocument?> GetByNumberAsync(string number)
        {
            return await _repository.GetByNumberAsync(number);
        }

        public IQueryable<ReceiptDocument> GetAllQuery()
        {
            return _repository.GetAllQuery();
        }

        public async Task<IEnumerable<ReceiptDocument>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ReceiptDocument?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsunc(id);
        }

        public async Task AddAsync(ReceiptDocument entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(ReceiptDocument entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ReceiptDocument>> GetByDateRangeAsync(DateTime from, DateTime to)
        {
            return await Task.FromResult(_repository.GetAllQuery().Where(r => r.Date >= from && r.Date <= to));
        }

        public async Task<IEnumerable<ReceiptDocument>> GetByResourcesAsync(List<int> resourceIds)
        {
            return await Task.FromResult(
                _repository.GetAllQuery()
                    .Where(doc => doc.ReceiptResources != null &&
                                  doc.ReceiptResources.Any(rr => rr.Resource != null && resourceIds.Contains(rr.Resource.Id)))
                    .ToList()
            );
        }

        public async Task<IEnumerable<ReceiptDocument>> GetByUnitsAsync(List<int> unitIds)
        {
            return await Task.FromResult(
                _repository.GetAllQuery()
                    .Where(doc => doc.ReceiptResources != null &&
                                  doc.ReceiptResources.Any(rr => rr.Unit != null && unitIds.Contains(rr.Unit.Id)))
                    .ToList()
            );
        }

        public async Task<IEnumerable<ReceiptDocument>> GetFilteredAsync(
    string number,
    List<int>? resourceIds,
    List<int>? unitIds,
    DateTime? from,
    DateTime? to)
        {
            // IQueryable — без IIncludableQueryable
            IQueryable<ReceiptDocument> query = _repository.GetAllQuery()
                .Include(d => d.ReceiptResources)
                    .ThenInclude(rr => rr.Resource)
                .Include(d => d.ReceiptResources)
                    .ThenInclude(rr => rr.Unit);

            if (!string.IsNullOrWhiteSpace(number))
                query = query.Where(d => d.Number.Contains(number));

            if (from.HasValue)
                query = query.Where(d => d.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(d => d.Date <= to.Value);

            if (resourceIds != null && resourceIds.Any())
                query = query.Where(d => d.ReceiptResources.Any(rr => rr.ResourceId != null && resourceIds.Contains(rr.ResourceId)));

            if (unitIds != null && unitIds.Any())
                query = query.Where(d => d.ReceiptResources.Any(rr => rr.UnitId != null && unitIds.Contains(rr.UnitId)));

            return await query.ToListAsync();
        }
    }
}




