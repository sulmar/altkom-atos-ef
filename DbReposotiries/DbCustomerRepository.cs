using IRepositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbReposotiries
{

    public class DbCustomerRepository : ICustomerRepository
    {
        private readonly ShopContext context;

        public DbCustomerRepository(ShopContext context)
        {
            this.context = context;
        }

        public void Add(Customer customer)
        {
            context.Customers.Add(customer); // <- dodanie obiektu do Contextu
            context.SaveChanges();           // <- wysłanie zapytania SQL do bazy danych
        }

        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
