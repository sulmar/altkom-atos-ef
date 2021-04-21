using Models;
using System;
using System.Collections.Generic;

namespace IRepositories
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        void AddRange(IEnumerable<Customer> customers);
        void RemoveRange(IEnumerable<Customer> customers);
        IEnumerable<CustomerByGender> GetByGenders();


    }
}
