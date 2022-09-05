using Microsoft.EntityFrameworkCore;
using SleekBoard.Entities;

namespace SleekBoard.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _context = null!;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _context.Attach(entity);
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> FindAsync(Guid id)
        {
            return (await _context.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id == id))!;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await FindAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}