using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProuctsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProuctsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
            : base(x => 
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower()
                    .Contains(productParams.Search)) &&
                (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || x.TypeId == productParams.TypeId)
            )
        {
            AddInclude(x => x.Type);
            AddInclude(x => x.Brand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1),
                productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
                switch(productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
        }

        public ProuctsWithTypesAndBrandsSpecification(int id)
            : base(x => x.Id == id)
        {            
            AddInclude(x => x.Type);
            AddInclude(x => x.Brand);
        }
    }
}