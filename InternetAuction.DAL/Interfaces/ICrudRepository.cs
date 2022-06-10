using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    public interface ICrudRepository<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> GetAsync();

        Task<TEntity> GetByIdAsync(int entityId);

        void Add(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteByIdAsync(int entityId);

        void Update(TEntity entity);
    }
}