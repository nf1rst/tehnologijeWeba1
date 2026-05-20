using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.DAL.Data
{
    public class HeavyArchivesContextFactory : IDesignTimeDbContextFactory<HeavyArchivesContext>
    {
        // bez ovoga se migracije ne zeel stvorit jer dotnet ef migrations add radi izvan aplikacije i nema pojma da sam ja u program.cs dodala neke
        //parametre da se to pokrene 
        public HeavyArchivesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HeavyArchivesContext>();

            optionsBuilder.UseSqlServer("Server=DESKTOP-R8V30UJ;User Id=sa;Password=SQL;TrustServerCertificate=true;Database=HeavyArchivesDB");
            //jer ovaj ne vidi ovo sto je zapisano tamo u appsettings


            return new HeavyArchivesContext(optionsBuilder.Options);
        }
    }
}

