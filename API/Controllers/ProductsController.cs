using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        #region Fields
        private readonly IRepository<Product> productRepo;
        private readonly IRepository<ProductBrand> productBrandRepo;
        private readonly IRepository<ProductType> productTypeRepo;
        private readonly IMapper mapper;
        #endregion

        #region Constructors
        public ProductsController(IRepository<Product> productRepo,
                                IRepository<ProductBrand> productBrandRepo,
                                IRepository<ProductType> productTypeRepo,
                                IMapper mapper)
        {
            this.productRepo = productRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
            this.mapper = mapper;
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(
            string sort, int? brandId, int? typeId)
        {
            var spec = new ProductsWithBrandsAndTypesSpec(sort, brandId, typeId);

            var products = await productRepo.ListAsync(spec);

            return Ok(products
                        .Select(product =>
                        mapper.Map<Product, ProductToReturnDto>(product)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpec(id);

            var product = await productRepo.GetEntityWithSpec(spec);

            if (product == null)
                return NotFound(new ApiResponse(404));

            return Ok(mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() =>
            Ok(await productBrandRepo.GetAllAsync());

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes() =>
            Ok(await productTypeRepo.GetAllAsync());
    }
}