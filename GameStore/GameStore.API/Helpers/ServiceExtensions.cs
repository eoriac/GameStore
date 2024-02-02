using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GameStore.API;

public static class ServiceExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
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
}
