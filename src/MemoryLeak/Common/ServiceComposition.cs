namespace MemoryLeak.Common;

public static class ServiceComposition
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IHotelService, HotelService>();
        services.AddSingleton<BadCacheManager>();
        services.AddSingleton<GoodCacheManager>();
        
        return services;
    }
}