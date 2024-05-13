using App.Desafio.Blog.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace App.Desafio.Blog.Application.Services
{
    public static class DatabaseManagementService
    {
        public static void MigrationInit(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();
                //if (context.Database.GetPendingMigrations().Any())
                //{
                    Log.Information("Execute migration.");
                    context.Database.Migrate();

                //}
            }
        }
    }
}
