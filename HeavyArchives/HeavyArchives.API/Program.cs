using EncyclopaediaBisemutum.API.Middleware;
using HeavyArchives.API.Extensions;
using HeavyArchives.Application.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

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

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT authorization header",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
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

        app.UseCustomExceptionHandling(); // ako je app.useExceptionHandler(); to je build in pa ce se sve raspast ako ne konfigurairam posebno


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        //app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();



        app.MapControllers();

       // app.UseMiddleware<RateLimitingMiddleware>();


        app.Run();
    }
}