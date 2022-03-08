using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IReadOnlyList<Product>> GetProductsAsync();
    Task<ProductType> GetProductTypeByIdAsync(int id);
    Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    Task<ProductBrand> GetProductBrandsByIdAsync(int id);
    Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
}
