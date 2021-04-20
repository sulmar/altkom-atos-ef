using IRepositories;
using Models;

namespace DbReposotiries
{
    public class DbOrderRepository : DbEntityRepository<Order>, IOrderRepository
    {
        public DbOrderRepository(ShopContext context) : base(context)
        {
        }
    }
}
