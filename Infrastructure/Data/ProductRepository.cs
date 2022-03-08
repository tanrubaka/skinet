using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;

        public ProductRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _storeContext.ProductBrands.ToListAsync();
        }

        public async Task<ProductBrand> GetProductBrandsByIdAsync(int id)
        {
            return await _storeContext.ProductBrands.FindAsync(id);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _storeContext.Products
                .Include(p => p.Type)
                .Include(p => p.Brand)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _storeContext.Products
                .Include(p => p.Type)
                .Include(p => p.Brand)
                .ToListAsync();
        }

        public async Task<ProductType> GetProductTypeByIdAsync(int id)
        {
            return await _storeContext.ProductTypes.FindAsync(id);
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _storeContext.ProductTypes.ToListAsync();
        }
    }
}