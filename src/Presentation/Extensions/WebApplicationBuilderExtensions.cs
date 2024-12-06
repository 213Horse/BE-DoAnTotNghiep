namespace CleanMinimalApi.Presentation.Extensions;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application;
using CleanMinimalApi.Infrastructure.Databases.Tourism;
using CleanMinimalApi.Infrastructure.Databases.Tourism.Models;
using FluentValidation;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

[ExcludeFromCodeCoverage]
public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureApplicationBuilder(this WebApplicationBuilder builder)
    {
        #region Logging

        _ = builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
        {
            var assembly = Assembly.GetEntryAssembly();

            _ = loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration)
                .Enrich.WithProperty(
                    "Assembly Version",
                    assembly?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version)
                .Enrich.WithProperty(
                    "Assembly Informational Version",
                    assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion);
        });

        #endregion Logging

        #region Serialisation

        _ = builder.Services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            opt.SerializerOptions.PropertyNameCaseInsensitive = true;
            opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });

        #endregion Serialisation

        #region Swagger

        var ti = CultureInfo.CurrentCulture.TextInfo;

        _ = builder.Services.AddEndpointsApiExplorer();
        _ = builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"CleanMinimalApi API - {ti.ToTitleCase(builder.Environment.EnvironmentName)}",
                    Description = "An example to share an implementation of Minimal API in .NET 6.",
                    Contact = new OpenApiContact
                    {
                        Name = "CleanMinimalApi API",
                        Email = "cleanminimalapi@stphnwlsh.dev",
                        Url = new Uri("https://github.com/stphnwlsh/cleanminimalapi")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "CleanMinimalApi API - License - MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    },
                    TermsOfService = new Uri("https://github.com/stphnwlsh/cleanminimalapi")
                });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            options.DocInclusionPredicate((name, api) => true);

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            });


            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        #endregion Swagger

        #region Validation

        _ = builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton);

        #endregion Validation

        #region Project Dependencies

        _ = builder.Services.AddInfrastructure();
        _ = builder.Services.AddApplication();

        _ = builder.Services.AddDbContext<TourismDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("TourismDB"))
           );
        #endregion Project Dependencies

        #region Authentication

        _ = builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TourismDbContext>()
                .AddDefaultTokenProviders();

        builder.Services.AddAuthorization();

        _ = builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
         .AddJwtBearer(options =>
         {
             options.SaveToken = true;
             options.RequireHttpsMetadata = false;
             options.TokenValidationParameters = new TokenValidationParameters()
             {
                 ValidateIssuerSigningKey = true,
                /* ValidateIssuer = true,
                 ValidateAudience = true,*/
                 ValidAudience = builder.Configuration["JWT:ValidAudience"],
                 ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
             };
    });
        #endregion Authentication

        #region Swagger
    
        #endregion Swagger

        return builder;
    }
}
