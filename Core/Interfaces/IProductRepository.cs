using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int id);

        Task<List<Product>> GetProducts();

        Task<List<ProductBrand>> GetProductsBrand();

        Task<List<ProductType>> GetProductsType();
    }
}