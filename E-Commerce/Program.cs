using _InfraStructure.Data;
using _InfraStructure.Repositories;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

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