using Core.Entities;
using Core.Spacifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> specification);

        Task<List<T>> AllAsync(ISpecification<T> specification);
    }
}