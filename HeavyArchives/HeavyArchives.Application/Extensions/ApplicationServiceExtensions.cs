using AutoMapper;
using EncyclopaediaBisemutum.Application.Interfaces;
using EncyclopaediaBisemutum.Application.Services;
using EncyclopaediaBisemutum.Application.Validators;
using FluentValidation;
using HeavyArchives.Application.DTOs.Person;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Application.Mapping;
using HeavyArchives.Application.Options;
using HeavyArchives.Application.Services;
using HeavyArchives.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

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
            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IValidator<ManipulatePersonDTO>, ManipulatePersonDTOValidator>();

            //https://docs.automapper.io/en/stable/Configuration.html

            // bez AutoMapper.Extensions.Microsoft.DependencyInjection paketa
            var configExpr = new MapperConfigurationExpression();
            configExpr.AddMaps(typeof(MappingProfile).Assembly);
            var mapperConfig = new MapperConfiguration(configExpr, NullLoggerFactory.Instance); 
            //ne moze se napravit objekt bez da se posalje logger al nije potrebno trenutno 
            services.AddSingleton(mapperConfig.CreateMapper());

            return services;
        }
    }
}
