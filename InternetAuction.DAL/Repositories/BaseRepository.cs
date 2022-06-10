using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetAuction.DAL.Repositories
{
    public class BaseRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class, IEntity
    {
        private protected readonly AuctionContext _context;
        private protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(AuctionContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public async Task DeleteByIdAsync(int entityId)
        {
            var entityToDelete = await GetByIdAsync(entityId);

            _dbSet.Remove(entityToDelete);
        }

        public void Update(TEntity entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}