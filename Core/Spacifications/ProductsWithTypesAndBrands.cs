using Core.Entities;

namespace Core.Spacifications
{
    public class ProductsWithTypesAndBrands : BaseSpacification<Product>
    {
        public ProductsWithTypesAndBrands()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        public ProductsWithTypesAndBrands(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}