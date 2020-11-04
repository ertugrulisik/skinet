using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFilterForCountSpec : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpec(ProductSpecParams productParams) :
            base(x =>
                 (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                 (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            
        }
    }
}