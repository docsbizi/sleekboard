using SleekBoard.Entities;

namespace SleekBoard.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task AddAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task<TEntity> FindAsync(Guid id);
        Task UpdateAsync(TEntity entity);
    }
}