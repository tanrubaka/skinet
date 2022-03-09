using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProuctsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProuctsWithTypesAndBrandsSpecification()
        {
            AddInclude(x => x.Type);
            AddInclude(x => x.Brand);
        }

        public ProuctsWithTypesAndBrandsSpecification(int id)
            : base(x => x.Id == id)
        {            
            AddInclude(x => x.Type);
            AddInclude(x => x.Brand);
        }
    }
}