using _InfraStructure.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Spacifications;
using Microsoft.EntityFrameworkCore;

namespace _InfraStructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context)
        {
            this.context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).SingleOrDefaultAsync();
        }

        public async Task<List<T>> AllAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
        }
    }
}