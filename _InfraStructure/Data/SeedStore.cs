using System.Text.Json;
using Core.Entities;

namespace _InfraStructure.Data;

public class SeedStore
{
    public static async Task SeedData(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var json = File.ReadAllText("../_InfraStructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(json);
            context.Products.AddRange(products);
        }

        if (!context.ProductBrands.Any())
        {
            var json = File.ReadAllText("../_InfraStructure/Data/SeedData/brands.json");
            var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(json);
            context.ProductBrands.AddRange(productBrands);
        }

        if (!context.Products.Any())
        {
            var json = File.ReadAllText("../_InfraStructure/Data/SeedData/types.json");
            var productTypes = JsonSerializer.Deserialize<List<ProductType>>(json);
            context.ProductTypes.AddRange(productTypes);
        }

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
}