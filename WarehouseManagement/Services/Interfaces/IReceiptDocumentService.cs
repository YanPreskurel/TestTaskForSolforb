using WarehouseManagement.Models.Entities;

namespace WarehouseManagement.Services.Interfaces
{
    public interface IReceiptDocumentService
    {
        Task<ReceiptDocument?> GetByNumberAsync(string number);
        IQueryable<ReceiptDocument> GetAllQuery();

        Task<IEnumerable<ReceiptDocument>> GetAllAsync();
        Task<ReceiptDocument?> GetByIdAsync(int id);
        Task AddAsync(ReceiptDocument entity);
        Task UpdateAsync(ReceiptDocument entity);
        Task DeleteAsync(int id);

        Task<IEnumerable<ReceiptDocument>> GetByDateRangeAsync(DateTime from, DateTime to);
        Task<IEnumerable<ReceiptDocument>> GetByResourcesAsync(List<int> resourceIds);
        Task<IEnumerable<ReceiptDocument>> GetByUnitsAsync(List<int> unitIds);

        Task<IEnumerable<ReceiptDocument>> GetAllWithIncludesAsync();
        Task<IEnumerable<ReceiptDocument>> GetFilteredAsync(string number, List<int>? resourceIds, List<int>? unitIds,DateTime? from, DateTime? to);
    }
}
