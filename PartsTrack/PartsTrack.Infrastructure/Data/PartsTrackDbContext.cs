using Microsoft.EntityFrameworkCore;

using PartsTrack.Domain.Entities;

namespace PartsTrack.Infrastructure.Data
{
    public class PartsTrackDbContext : DbContext
    {
        public PartsTrackDbContext(DbContextOptions<PartsTrackDbContext> options) : base(options)
        {
        }

        public DbSet<Part> Parts { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Inventory> Inventory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring Part entity
            modelBuilder.Entity<Part>(entity =>
            {
                entity.HasKey(p => p.PartId);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description).HasMaxLength(255);
                entity.Property(p => p.Price).IsRequired().HasPrecision(10, 2);
                entity.Property(p => p.StockQuantity).IsRequired();
            });

            // Configuring Warehouse entity
            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasKey(w => w.WarehouseId);
                entity.Property(w => w.Location).IsRequired().HasMaxLength(100);
                entity.Property(w => w.Capacity).IsRequired();
            });

            // Configuring Inventory entity
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(i => i.InventoryId);
                entity.Property(i => i.Quantity).IsRequired();

                // Setting up foreign key relationships
                entity.HasOne<Part>()
                    .WithMany()
                    .HasForeignKey(i => i.PartId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Warehouse>()
                    .WithMany()
                    .HasForeignKey(i => i.WarehouseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
