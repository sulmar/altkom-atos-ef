using Models;
using Models.SearchCriterias;
using System;
using System.Collections.Generic;

namespace IRepositories
{
    public interface IOrderRepository : IEntityRepository<Order>
    {
        void AddRange(IEnumerable<Order> orders);
        IEnumerable<Order> Calculate();
        IEnumerable<Order> Get(OrderSearchCriteria searchCriteria);
    }
}
