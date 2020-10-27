using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository context;

        public ProductsController(IProductRepository repo) =>
            this.context = repo;

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() =>
            Ok(await context.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) =>
            Ok(await context.GetAsync(id));

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() =>
            Ok(await context.GetProductBrandsAsync());

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes() =>
            Ok(await context.GetProductTypesAsync());
    }
}