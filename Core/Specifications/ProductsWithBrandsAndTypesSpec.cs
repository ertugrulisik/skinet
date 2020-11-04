using System.Security.Cryptography.X509Certificates;
using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithBrandsAndTypesSpec : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpec(string sort, int? brandId, int? typeId)
            : base(x =>
                 (!brandId.HasValue || x.ProductBrandId == brandId) &&
                 (!typeId.HasValue || x.ProductTypeId == typeId)
            )
        {
            AddIncludes();
            AddSort(sort);
        }

        public ProductsWithBrandsAndTypesSpec(int id) :
            base(x => x.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        private void AddSort(string sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }
    }
}