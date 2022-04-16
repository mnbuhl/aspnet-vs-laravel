using Orders.Application.Interfaces;

namespace Orders.Api.Jobs;

public static class BackgroundJobsRegistration
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddHostedService<BackgroundJobs>();
        services.AddScoped<IShippingStatusJob, CheckShippingStatus>();

        return services;
    }
}