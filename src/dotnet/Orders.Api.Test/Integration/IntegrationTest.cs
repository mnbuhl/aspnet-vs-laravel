using System;
using System.Linq;
using HttpMessenger.Service;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Interfaces;
using Orders.Infrastructure.Data;
using DatabaseTransaction = Orders.Api.Test.Mocks.DatabaseTransaction;

namespace Orders.Api.Test.Integration;

public class IntegrationTest : IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    protected readonly IHttpMessenger Messenger;

    protected IntegrationTest()
    {
        var appFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var contextDescriptor = services.FirstOrDefault(x =>
                        x.ServiceType == typeof(DbContextOptions<AppDbContext>));

                    var transactionDescriptor =
                        services.FirstOrDefault(x => x.ServiceType == typeof(IDatabaseTransaction));

                    if (contextDescriptor != null)
                    {
                        services.Remove(contextDescriptor);
                    }

                    if (transactionDescriptor != null)
                    {
                        services.Remove(transactionDescriptor);
                    }

                    services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TestDb"));
                    services.AddScoped<IDatabaseTransaction, DatabaseTransaction>();
                });
            });

        _serviceProvider = appFactory.Services;
        var httpClient = appFactory.CreateClient();
        httpClient.BaseAddress = new Uri("https://localhost:7016/api/v1/");

        Messenger = new HttpMessenger.Service.HttpMessenger(httpClient);

        using var scope = appFactory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        using var scope = _serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.EnsureDeleted();
    }
}