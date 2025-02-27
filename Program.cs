using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.EntityFrameworkCore;
using MVCMovieApp.Data;
using MVCMovieApp.Migrations;
using System.Reflection;

namespace MVCMovieApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var dbConnectionString = builder.Configuration.GetConnectionString("MVCMovieContext");
           var dbConnectionResult =  builder.Services.AddDbContext<MVCMovieAppDBContext>(
                option => option.UseSqlServer(dbConnectionString ?? throw new InvalidOperationException("ConnectionString 'MVCMovieContext' not found!")));

            builder.Services.AddFluentMigratorCore()
                   .ConfigureRunner(rb => rb
                          .AddSqlServer()
                          .WithGlobalConnectionString(dbConnectionString)
                          .WithMigrationsIn(Assembly.GetExecutingAssembly()))
                   // Define the assembly containing the migrations
                   .Configure<TypeFilterOptions>(option => option.Namespace = "MVCMovieApp.Migrations")
                   // Enable logging to console in the FluentMigrator way
                   .AddLogging(lb => lb.AddFluentMigratorConsole());

            builder.Services.AddScoped<IMigrations, Migrations.Migrations>();
            builder.Services.AddScoped<IMigrationRunner, MigrationRunner>();
            
            bool migrated = false;
            
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrations>();
                migrated = runner.MigrationRunner();
            }
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
