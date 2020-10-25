using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext context;

        public ProductsController(StoreContext context) => 
            this.context = context;

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() =>
            Ok(await context.Products.ToListAsync());
            

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) => 
            Ok(await context.Products.FindAsync(id));
    }
}