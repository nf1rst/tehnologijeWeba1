using HeavyArchives.API.Extensions;
using HeavyArchives.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.


        builder.Services.SetDALServices(
            builder.Configuration.GetConnectionString("HeavyConnection")!);

        builder.Services.SetApplicationServices(builder.Configuration);


        builder.Services.AddJwtAuthentication(builder.Configuration);
        ////////////////////////////////////////

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Heavy Archives API",
                Version = "v1",
                Description = "Clean architecture API s JWT autentifikacijom",
                Contact = new OpenApiContact
                {
                    Name = "Algebra",
                    Email = "info@algebra.hr"
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT authorization header",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
            });
        });

        builder.Host.UseSerilog(
            (context, config) => config.ReadFrom.Configuration(context.Configuration));
        /////////////////////////////////////////////// da se pojavi ono u 
        ///

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCustomExceptionHandling(); // ako je app.useExceptionHandler(); to je build in pa ce se sve raspast ako ne konfigurairam posebno

        //app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}