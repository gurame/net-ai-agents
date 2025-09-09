using InvoiceApp.Database;
using InvoiceApp.Repositories;
using InvoiceApp.Services;

namespace InvoiceApp;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbConnectionFactory>(sp=>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return new SqliteConnectionFactory(connectionString!);
        });
        services.AddSingleton<DbInitializer>();
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        return services;
    }
    
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IInvoiceRepository, InvoiceRepository>();
        services.AddSingleton<IInvoiceService, InvoiceService>();
        return services;
    }
}