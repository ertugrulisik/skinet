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
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetAsync(int id) =>
             await _context.Products.FindAsync(id);
        

        public async Task<IReadOnlyList<Product>> GetAllAsync() =>
            await _context.Products.ToListAsync();
    
    }
}