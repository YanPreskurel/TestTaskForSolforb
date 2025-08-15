using WarehouseManagement.Models.Entities;

namespace WarehouseManagement.Interfaces
{
    public interface IReceiptDocumentRepository : IBaseRepository<ReceiptDocument>
    {
        Task<ReceiptDocument?> GetByNumberAsync(string number); // Для проверки уникальности номера
        IQueryable<ReceiptDocument> GetAllQuery();
    }
}
