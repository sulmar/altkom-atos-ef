using IRepositories;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace DbReposotiries
{
    public class DbOrderRepository : DbEntityRepository<Order>, IOrderRepository
    {
        public DbOrderRepository(ShopContext context) : base(context)
        {
        }

        public void AddRange(IEnumerable<Order> orders)
        {
            entities.AddRange(orders);

            var entries = context.ChangeTracker.Entries()
                .Select(e => new { e.Entity.GetType().Name, e.State});

            
            context.SaveChanges();
        }
    }
}
