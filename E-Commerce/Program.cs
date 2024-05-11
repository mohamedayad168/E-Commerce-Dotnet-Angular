using _InfraStructure.Data;
using E_Commerce.Extensions;
using E_Commerce.MiddleWare;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddApplicationServices(builder.Configuration);

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleWare>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.MapControllers();

            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var context = service.GetRequiredService<StoreContext>();
            var logger = service.GetRequiredService<ILogger<Program>>();
            try
            {
                await context.Database.MigrateAsync();
                await SeedStore.SeedData(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error Occuired");
            }

            app.Run();
        }
    }
}