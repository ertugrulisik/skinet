using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public Repository(StoreContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() =>
            await context.Set<T>().ToListAsync();

        public async Task<T> GetAsync(int id) =>
            await context.Set<T>().FindAsync(id);

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec) =>
            await ApplySpecification(spec).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec) =>
            await ApplySpecification(spec).ToListAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> spec) =>
            SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);

        public async Task<int> CountAsync(ISpecification<T> spec) =>
            await ApplySpecification(spec).CountAsync();
    }
}