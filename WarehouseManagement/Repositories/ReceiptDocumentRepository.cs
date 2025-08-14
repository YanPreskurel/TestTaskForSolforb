using WarehouseManagement.Data;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WarehouseManagement.Repositories
{
    public class ReceiptDocumentRepository : BaseRepository<ReceiptDocument>, IReceiptDocumentRepository
    {
        public ReceiptDocumentRepository(AppDbContext context) : base(context) { }

        public IQueryable<ReceiptDocument> GetAllQuery()
        {
            return _context.Set<ReceiptDocument>().AsQueryable();
        }

        public async Task<ReceiptDocument?> GetByNumberAsync(string number)
        {
            return await _context.ReceiptDocuments.FirstOrDefaultAsync(r => r.Number == number);
        }
    }
}
