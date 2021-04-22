using System.Collections.Generic;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IEntityRepositoryAsync<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(int id);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int id);
    }
}
