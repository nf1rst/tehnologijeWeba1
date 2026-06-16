using HeavyArchives.DAL.Data;
using HeavyArchives.DAL.Repositories;
using HeavyArchives.DAL.Repositories.M2M;
using HeavyArchives.Domain.Interfaces;
using HeavyArchives.Domain.Interfaces.M2M;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HeavyArchives.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection SetDALServices(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<HeavyArchivesContext>(options => options.UseSqlServer(connectionString));

           //https://www.linkedin.com/pulse/understanding-addtransient-vs-addscoped-addsingleton-aspnet-shakkouf-nxraf#:~:text=AddScoped%3A%20Scoped%20lifetime%20services%20are,will%20use%20the%20same%20instance.
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
            services.AddScoped<IBandLabelRepository, BandLabelRepository>();
            services.AddScoped<IBandPersonRepository, BandPersonRespository>();
            services.AddScoped<IBandThemeRepository, BandThemeRepository>();
            services.AddScoped<IBandTourRepository, BandTourRepository>();
            services.AddScoped<IGenreBandRepository, GenreBandRepository>();
            services.AddScoped<ILabelThemeRepository, LabelThemeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
           

            return services;
        }
    }
}
