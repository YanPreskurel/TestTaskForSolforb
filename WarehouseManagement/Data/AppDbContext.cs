using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Models.Entities;

namespace WarehouseManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<ReceiptDocument> ReceiptDocuments { get; set; }
        public DbSet<ReceiptResource> ReceiptResources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Уникальность названия для ресурсов
            modelBuilder.Entity<Resource>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // Уникальность названия для единиц измерения
            modelBuilder.Entity<Unit>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
