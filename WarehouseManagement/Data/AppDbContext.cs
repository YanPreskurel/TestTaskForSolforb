using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WarehouseManagement.Models.Entities;

namespace WarehouseManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ReceiptDocument> ReceiptDocuments { get; set; }
        public DbSet<ReceiptResource> ReceiptResources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Тестовые единицы измерения
            modelBuilder.Entity<Unit>().HasData(
                new Unit { Id = 1, Name = "Килограмм", IsActive = true },
                new Unit { Id = 2, Name = "Литр", IsActive = true },
                new Unit { Id = 3, Name = "Штука", IsActive = true }
            );

            // Тестовые ресурсы
            modelBuilder.Entity<Resource>().HasData(
                new Resource { Id = 1, Name = "Сахар", IsActive = true },
                new Resource { Id = 2, Name = "Молоко", IsActive = true },
                new Resource { Id = 3, Name = "Яйца", IsActive = true }
            );

            // Тестовые документы поступления
            modelBuilder.Entity<ReceiptDocument>().HasData(
                new ReceiptDocument { Id = 1, Number = "001", Date = DateTime.Today.AddDays(-5) },
                new ReceiptDocument { Id = 2, Number = "002", Date = DateTime.Today.AddDays(-2) }
            );

            // Тестовые ресурсы в документах поступления
            modelBuilder.Entity<ReceiptResource>().HasData(
                new ReceiptResource { Id = 1, ReceiptDocumentId = 1, ResourceId = 1, UnitId = 1, Quantity = 100 },
                new ReceiptResource { Id = 2, ReceiptDocumentId = 1, ResourceId = 2, UnitId = 2, Quantity = 50 },
                new ReceiptResource { Id = 3, ReceiptDocumentId = 2, ResourceId = 3, UnitId = 3, Quantity = 200 }
            );
            // Уникальность названия для ресурсов
            modelBuilder.Entity<Resource>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // Уникальность названия для единиц измерения
            modelBuilder.Entity<Unit>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

    }
}
