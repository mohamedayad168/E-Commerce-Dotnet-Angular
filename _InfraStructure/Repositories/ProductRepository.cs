using _InfraStructure.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _InfraStructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext dbContext;

    public ProductRepository(StoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Product> GetProductById(int id)
    {
        return await dbContext.Products.Include(p => p.ProductBrand)
            .Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetProducts()
    {
        return await dbContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
    }

    public async Task<List<ProductBrand>> GetProductsBrand()
    {
        return await dbContext.ProductBrands.ToListAsync();
    }

    public async Task<List<ProductType>> GetProductsType()
    {
        return await dbContext.ProductTypes.ToListAsync();
    }
}