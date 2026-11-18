using AutoMapper;
using EncyclopaediaBisemutum.Application.DTOs.Auth;
using EncyclopaediaBisemutum.Application.Interfaces;
using EncyclopaediaBisemutum.Application.Services;
using EncyclopaediaBisemutum.Application.Validators;
using FluentValidation;
using HeavyArchives.Application.DTOs.Contact;
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
                configuration.GetSection(JwtSettings.SectionName));

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
            services.AddScoped<ICityService, CityService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IValidator<ManipulatePersonDTO>, ManipulatePersonDTOValidator>();
            services.AddScoped<IValidator<string>, StringNotEmptyValidator>();
            services.AddScoped<IValidator<UserRegisterWithDetailsDTO>, RegisterWithDetailsDTOValidator>();
            services.AddScoped<IValidator<ManipulateContactDTO>, ManipulateContactDTOValidator>();

            return services;
        }
    }
}
