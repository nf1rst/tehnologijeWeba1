using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HeavyArchives.DAL.Data
{
    public class HeavyArchivesContextFactory : IDesignTimeDbContextFactory<HeavyArchivesContext>
    {
        // bez ovoga se migracije ne zeel stvorit jer dotnet ef migrations add radi izvan aplikacije i nema pojma da sam ja u program.cs dodala neke
        //parametre da se to pokrene 

        //https://stackoverflow.com/questions/50820275/how-to-use-idesigntimedbcontextfactory-implementation-in-asp-net-core-2-1
        //https://www.reddit.com/r/dotnet/comments/1lzr7dx/how_works_idesigntimedbcontextfactory_in_an_asp/

        //dotnet ef migrations add InitDatabase --project Project.DataAccess --startup-project Project.API
        public HeavyArchivesContext CreateDbContext(string[] args)
        { 
            var optionsBuilder = new DbContextOptionsBuilder<HeavyArchivesContext>();

            optionsBuilder.UseSqlServer("Server=NINOSLAV;User Id=sa;Password=SQL;TrustServerCertificate=true;Database=HeavyArchivesDB");
            //jer ovaj ne vidi ovo sto je zapisano tamo u appsettings


            return new HeavyArchivesContext(optionsBuilder.Options);
        }
    }
}

