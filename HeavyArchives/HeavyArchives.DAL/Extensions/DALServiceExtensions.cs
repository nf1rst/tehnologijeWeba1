using HeavyArchives.DAL.Data;
using HeavyArchives.DAL.Repositories;
using HeavyArchives.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HeavyArchives.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection SetDALServices(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<HeavyArchivesContext>(options => options.UseSqlServer("Server=DESKTOP-R8V30UJ;User Id=sa;Password=SQL;TrustServerCertificate=true;Database=HeavyArchivesDB"));
            //ovaj ne vidi appsettings pa se mora rucno string stavit?

            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IBandRepository, BandRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ILabelRepository, LabelRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IThemeRepository, ThemeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IConcertRepository, ConcertRepository>();
            services.AddScoped<ITourRepository, TourRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IBandMemberStatusRepository, BandMemberStatusRepository>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
           

            return services;
        }
    }
}
