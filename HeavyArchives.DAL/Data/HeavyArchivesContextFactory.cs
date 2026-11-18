using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HeavyArchives.DAL.Data
{
    public class HeavyArchivesContextFactory : IDesignTimeDbContextFactory<HeavyArchivesContext>
    {
        public HeavyArchivesContext CreateDbContext(string[] args)
        { 
            var optionsBuilder = new DbContextOptionsBuilder<HeavyArchivesContext>();

            optionsBuilder.UseSqlServer("Server=NINOSLAV;User Id=sa;Password=SQL;TrustServerCertificate=true;Database=HeavyArchivesDB");

            return new HeavyArchivesContext(optionsBuilder.Options);
        }
    }
}

