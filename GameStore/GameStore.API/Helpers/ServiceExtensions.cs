using System.Reflection;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace GameStore.API;

public static class ServiceExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {

        /*
        var dbUserName = Environment.GetEnvironmentVariable("DB_USERNAME");
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
        */

        services.Configure<GameStoreDatabaseSettings>(configuration.GetSection("GameStoreDb"));

        services.AddSingleton(serviceProvider => {

            var options = serviceProvider.GetService<IOptions<GameStoreDatabaseSettings>>() ?? throw new Exception("Missing GameStoreDb configuration");

            return new MongoClient(options.Value.ConnectionString);
        });

        services.AddSingleton(serviceProvider => {
            var options = serviceProvider.GetService<IOptions<GameStoreDatabaseSettings>>() ?? throw new Exception("Missing GameStoreDb configuration");
            var client = serviceProvider.GetService<MongoClient>() ?? throw new Exception("Missing MongoDb client");

            return client.GetDatabase(options.Value.DatabaseName);    
        });

        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options => {

            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
            options.IncludeXmlComments(xmlCommentsFullPath);

            options.AddSecurityDefinition("GamesApiBearerAuth", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,                    
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "A valid token"
                });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "GamesApiBearerAuth"
                        }
                    }, new List<string>()
                }
            });   
        });        

        services.ConfigureOptions<SwaggerOptions>();

        return services;
    }

    public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services){

        services.AddApiVersioning(setupAction =>
        {
            setupAction.AssumeDefaultVersionWhenUnspecified = true;
            setupAction.DefaultApiVersion = new ApiVersion(1, 0);
            setupAction.ReportApiVersions = true;
            setupAction.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(), // /api/v1/*
                new HeaderApiVersionReader("x-api-version"), // x-api-version:1.0
                new MediaTypeApiVersionReader("x-api-version")); // Accept/Content-Type: application/json; x-api-version=1.0
        })
        .AddApiExplorer(setup =>
        {
            setup.GroupNameFormat = $"'{Assembly.GetExecutingAssembly().GetName().Name} v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });    

        return services;    
    }
}
