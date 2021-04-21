using Models;
using System.Collections.Generic;

namespace IRepositories
{
    public interface IOrderRepository : IEntityRepository<Order>
    {
        void AddRange(IEnumerable<Order> orders);
    }
}
