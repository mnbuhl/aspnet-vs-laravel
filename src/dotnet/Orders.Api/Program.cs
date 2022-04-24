using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Api.Jobs;
using Orders.Api.Services;
using Orders.Application.Interfaces;
using Orders.Infrastructure.Data;
using Orders.Infrastructure.Repositories;
using System.Text.Json.Serialization;

namespace Orders.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(opt
            => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IDatabaseTransaction, DatabaseTransaction>();

        builder.Services.AddApiVersioning(x =>
        {
            x.AssumeDefaultVersionWhenUnspecified = true;
            x.DefaultApiVersion = new ApiVersion(1, 0);
            x.ReportApiVersions = true;
        });

        builder.Services.AddScoped<ICarrierService, CarrierService>();

        builder.Services.AddBackgroundJobs();

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        if (context.Database.ProviderName != null && context.Database.ProviderName.Contains("PostgreSQL"))
        {
            context.Database.Migrate();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}