using System.Collections.Generic;

namespace IRepositories
{
    public interface IEntityRepository<TEntity>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
    }
}
