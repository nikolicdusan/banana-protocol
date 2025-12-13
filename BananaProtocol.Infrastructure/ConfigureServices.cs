using BananaProtocol.Application.Common.Interfaces;
using BananaProtocol.Infrastructure.Persistence;
using BananaProtocol.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BananaProtocol.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenProvider, TokenProvider>();

        services.AddDbContext<ApplicationDbContext>(options => SetDbContextOptions(options, configuration));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    #region Private Functions

    private static DbContextOptionsBuilder SetDbContextOptions(DbContextOptionsBuilder optionsBuilder, IConfiguration configuration) =>
        configuration.GetValue<bool>("UseInMemoryDatabase")
            ? optionsBuilder.UseInMemoryDatabase("BananaProtocol")
            : optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

    #endregion
}