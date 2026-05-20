using HeavyArchives.Application.Interfaces;
using HeavyArchives.Application.Mapping;
using HeavyArchives.Application.Options;
using HeavyArchives.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeavyArchives.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection SetApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<JwtSettings>(
                configuration.GetSection(JwtSettings.SectionName)); // za ovo treba Microsoft.Extensions.Options.ConfigurationExtensions

            services.AddScoped<IBandService, BandService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ILabelService, LabelService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IThemeService, ThemeService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IConcertService, ConcertService>();
            services.AddScoped<IBandMemberStatusService, BandMemberStatusService>();
            services.AddScoped<ITourService, TourService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddAutoMapper(typeof(MappingProfile).Assembly); // sto ovo znaci

            //ovdje ce ic validatori

            return services;
        }
    }
}
