using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersCountSpecification(
            ProductSpecParams productParams)
             : base(x=> 
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower()
                    .Contains(productParams.Search)) &&
                (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || x.TypeId == productParams.TypeId)
            )
        {
            
        }
    }
}