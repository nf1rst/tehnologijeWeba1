using Microsoft.EntityFrameworkCore;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Entities.M2MTables;
using Microsoft.EntityFrameworkCore.Metadata;
using EncyclopaediaBisemutum.Domain.Entities;
namespace HeavyArchives.DAL.Data
{
    public class HeavyArchivesContext:DbContext
    {
        public HeavyArchivesContext(DbContextOptions<HeavyArchivesContext> options):base(options){}
        public DbSet<Band> Bands{ get; set; }
        public DbSet<Person> Persons{ get; set; }
        public DbSet<Album> Albums{ get; set; }
        public DbSet<Contact> Contacts{ get; set; }
        public DbSet<Country> Countries{ get; set; }
        public DbSet<Genre> Genres{ get; set; }
        public DbSet<Label> Labels{ get; set; }
        public DbSet<Theme> Themes{ get; set; }
        public DbSet<BandLabel> BandLabels{ get; set; }
        public DbSet<BandPerson> BandPersons{ get; set; }
        public DbSet<BandTheme> BandThemes{ get; set; }
        public DbSet<GenreBand> GenreBands{ get; set; }
        public DbSet<LabelTheme> LabelTheme{ get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Role> Roles{ get; set; }
        public DbSet<Tour> Tours{ get; set; }
        public DbSet<BandTour>BandTours{ get; set; }
        public DbSet<Concert> Concerts{ get; set; }
        public DbSet<City> Cities{ get; set; }
        public DbSet<BandMemberStatus> Statuses{ get; set; }
        public DbSet<Log> Logs{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().ToTable("Genres");
            modelBuilder.Entity<Album>().ToTable("Albums");
            modelBuilder.Entity<Band>().ToTable("Bands");
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Concert>().ToTable("Concerts");
            modelBuilder.Entity<Contact>().ToTable("Contacts");
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Label>().ToTable("Labels");
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Theme>().ToTable("Themes");
            modelBuilder.Entity<Tour>().ToTable("Tours");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<BandLabel>().ToTable("BandLabels");
            modelBuilder.Entity<BandPerson>().ToTable("BandPersons");
            modelBuilder.Entity<BandTheme>().ToTable("BandThemes");
            modelBuilder.Entity<BandTour>().ToTable("BandTours");
            modelBuilder.Entity<GenreBand>().ToTable("GenreBands");
            modelBuilder.Entity<LabelTheme>().ToTable("LabelThemes");
            modelBuilder.Entity<BandMemberStatus>().ToTable("Statuses");
            modelBuilder.Entity<Log>().ToTable("Logs");

            modelBuilder.Entity<Band>()
                .HasMany(b => b.Albums)
                .WithOne(a => a.Band)
                .HasForeignKey(a => a.BandId);

            modelBuilder.Entity<Band>()
                .HasOne(b => b.Country)
                .WithMany(c => c.Bands)
                .HasForeignKey(b => b.CountryId);

            modelBuilder.Entity<Band>()
                .HasMany(b => b.GenreBand)
                .WithOne(gb => gb.Band)
                .HasForeignKey(b => b.BandId);

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.GenreBand)
                .WithOne(gb => gb.Genre)
                .HasForeignKey(gb=>gb.GenreId);

            modelBuilder.Entity<Band>()
                .HasMany(b => b.BandPerson)
                .WithOne(bp => bp.Band)
                .HasForeignKey(db => db.BandId);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.BandPerson)
                .WithOne(bp => bp.Person)
                .HasForeignKey(bp => bp.PersonId);

            modelBuilder.Entity<Band>()
                .HasMany(b => b.BandLabel)
                .WithOne(bl => bl.Band)
                .HasForeignKey(bl => bl.BandId);

            modelBuilder.Entity<Label>()
                .HasMany(l => l.BandLabel)
                .WithOne(bl => bl.Label)
                .HasForeignKey(bl => bl.LabelId);

    
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Band)
                .WithOne(b => b.Contact)
                .HasForeignKey<Contact>(c => c.BandId); 


            modelBuilder.Entity<Band>()
                .HasMany(b => b.BandTheme)
                .WithOne(bt => bt.Band)
                .HasForeignKey(bt => bt.BandId);

            modelBuilder.Entity<Theme>()
                .HasMany(t => t.BandTheme)
                .WithOne(bt => bt.Theme)
                .HasForeignKey(bt => bt.ThemeId);

            modelBuilder.Entity<Theme>()
                .HasMany(t => t.LabelTheme)
                .WithOne(bt => bt.Theme)
                .HasForeignKey(bt => bt.ThemeId);

            modelBuilder.Entity<Label>()
                .HasMany(l => l.LabelTheme)
                .WithOne(lt => lt.Label)
                .HasForeignKey(lt => lt.LabelId);


            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u=>u.RoleID);
            
            modelBuilder.Entity<Role>()
                .Property(r => r.Id)
                .ValueGeneratedNever(); 
     

            modelBuilder.Entity<Role>() 
                .HasData(
                new Role
                {
                    Id=1,
                    Name = "Admin"
                },
                new Role
                {
                    Id=2,
                    Name = "User"
                });

            modelBuilder.Entity<BandTour>()
                .HasOne(tb => tb.Tour)
                .WithMany(b => b.TourBand)
                .HasForeignKey(tb => tb.TourID); 

            modelBuilder.Entity<BandTour>()
                .HasOne(tb => tb.Band)
                .WithMany(b => b.TourBand)
                .HasForeignKey(tb => tb.BandId); 

            modelBuilder.Entity<Tour>()
                .HasMany(t => t.Concerts)
                .WithOne(c => c.Tour)
                .HasForeignKey(t => t.TourId);

            modelBuilder.Entity<City>()
                .HasMany(city => city.Concerts)
                .WithOne(concert => concert.City)
                .HasForeignKey(concert => concert.CityId);

            modelBuilder.Entity<Country>()
                .HasMany(country => country.Cities)
                .WithOne(city => city.Country)
                .HasForeignKey(city => city.CountryId);

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.TourManager)
                .WithMany(p => p.ManagedTours)
                .HasForeignKey(t => t.TourManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<BandMemberStatus>()
                .HasMany(s => s.BandPerson)
                .WithOne(bp => bp.Status)
                .HasForeignKey(bp => bp.StatusID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
