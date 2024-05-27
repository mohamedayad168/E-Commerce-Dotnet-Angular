using _InfraStructure.Data;
using _InfraStructure.Repositories;
using Core.Interfaces;
using E_Commerce.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace E_Commerce.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection Services,
        IConfiguration config)
    {
        Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        Services.AddEndpointsApiExplorer();
        Services.AddSwaggerGen();
        Services.AddDbContext<StoreContext>(options =>
        {
            options.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        Services.AddSingleton<IConnectionMultiplexer>(c =>
        {
            var option = ConfigurationOptions.Parse(config.GetConnectionString("Redis")!);
            return ConnectionMultiplexer.Connect(option);
        });
        Services.AddScoped<IProductRepository, ProductRepository>();
        Services.AddScoped<IBasketRepository, BasketRepository>();
        Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState.Where(x => x.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });
        Services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy",
                options => { options.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"); });
        });

        return Services;
    }
}