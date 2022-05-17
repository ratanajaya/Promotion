using Microsoft.EntityFrameworkCore;
using PromotionAPI.Models;

namespace PromotionAPI.Services
{
    public class AppDbContext : DbContext
    {
        GlobalVariable _global;

        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<RelPromotionStore> RelPromotionStores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder option) {
            option.UseNpgsql(_global.ConnectionString);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, GlobalVariable global) : base(options) {
            _global = global;
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<RelPromotionStore>()
                .HasKey(a => new { a.PromotionId, a.StoreId });
            builder.Entity<RelPromotionStore>()
                .HasOne(rel => rel.Promotion)
                .WithMany(p => p.RelPromotionStores)
                .HasForeignKey(rel => rel.PromotionId);

            builder.Entity<Store>().HasData(
                new Store { StoreId = "111", Name = "Toko Jakarta" },
                new Store { StoreId = "222", Name = "Toko Tangerang" },
                new Store { StoreId = "333", Name = "Toko Bogor" },
                new Store { StoreId = "444", Name = "Toko Bandung" }
            );

            base.OnModelCreating(builder);
        }
    }
}
