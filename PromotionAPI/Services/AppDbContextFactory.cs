using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PromotionAPI.Services
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(AppSettingJson.GetConnectionString());

            return new AppDbContext(optionsBuilder.Options, AppSettingJson.GetGlobalVariable());
        }
    }
}
