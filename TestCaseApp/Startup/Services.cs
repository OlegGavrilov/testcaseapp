using TestCaseApp.Services;

namespace TestCaseApp.Startup;

public static class Services
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<TokenService>();
        return services;
    }
}