using DbUp;
using ItemStore.WebApi.Clients;
using ItemStore.WebApi.Contexts;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Middlewares;
using ItemStore.WebApi.Profiles;
using ItemStore.WebApi.Repositories;
using ItemStore.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;
using System.Data;
using System.Reflection;

namespace ItemStore.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register services, repos

        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<IShopRepository, ShopRepository>();
        builder.Services.AddScoped<IShopItemRepository, ShopItemRepository>();
        builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();

        builder.Services.AddScoped<ItemService>();
        builder.Services.AddScoped<ShopService>();

        // External API
        builder.Services.AddHttpClient();
        builder.Services.AddTransient<IJsonPlaceholderClient, JsonPlaceholderClient>();

        builder.Services.AddScoped<UserService>();

        // DB things: Dapper, EF, DBUp
        // builder.Services.AddDbContext<PostgreContext>(options =>
        //     options.UseNpgsql(builder.Configuration.GetConnectionString("EFPostgreConnection") ?? throw new InvalidOperationException("Connection string not found.")));

        string dbConnectionString = builder.Configuration.GetConnectionString("PostgreConnection") ?? throw new ArgumentNullException();
        builder.Services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(dbConnectionString));

        EnsureDatabase.For.PostgresqlDatabase(dbConnectionString);

        var upgrader = DeployChanges.To
            .PostgresqlDatabase(dbConnectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToNowhere()
            .Build();
        var result = upgrader.PerformUpgrade();

        // AutoMapper

        builder.Services.AddAutoMapper(typeof(ItemProfile));
        builder.Services.AddAutoMapper(typeof(UserProfile));
        builder.Services.AddAutoMapper(typeof(ShopProfile));

        // Serilog

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
