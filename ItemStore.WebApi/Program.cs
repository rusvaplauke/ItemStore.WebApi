
using DbUp;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Repositories;
using ItemStore.WebApi.Services;
using Npgsql;
using Serilog;
using System.Data;
using System.Reflection;

namespace ItemStore.WebApi
{
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

            builder.Services.AddScoped<IItemRepository,ItemRepository>();
            builder.Services.AddScoped<IItemService, ItemService>();


            string dbConnectionString = builder.Configuration.GetConnectionString("PostgreConnection");
            builder.Services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(dbConnectionString));

            EnsureDatabase.For.PostgresqlDatabase(dbConnectionString);

            var upgrader = DeployChanges.To
                .PostgresqlDatabase(dbConnectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToNowhere()
                .Build();
            var result = upgrader.PerformUpgrade();

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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
