using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Identity.Microservice;

public static class IdentityServiceExtensions
{
    public static IServiceCollection RegisterDataLayer(this IServiceCollection services, IConfiguration configuration)
    {

        /*
        var dbUserName = Environment.GetEnvironmentVariable("DB_USERNAME");
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
        */

        services.Configure<IdentityMicroserviceDbSettings>(configuration.GetSection("IdentityMongoDb"));

        services.AddSingleton(serviceProvider => {

            var options = serviceProvider.GetService<IOptions<IdentityMicroserviceDbSettings>>() ?? throw new Exception("Missing GameStoreDb configuration");

            return new MongoClient(options.Value.ConnectionString);
        });

        services.AddSingleton(serviceProvider => {
            var options = serviceProvider.GetService<IOptions<IdentityMicroserviceDbSettings>>() ?? throw new Exception("Missing GameStoreDb configuration");
            var client = serviceProvider.GetService<MongoClient>() ?? throw new Exception("Missing MongoDb client");

            return client.GetDatabase(options.Value.DatabaseName);    
        });

        services.AddScoped<IUserRepository, UserRepository>();


        return services;
    }
}
