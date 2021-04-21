using IRepositories;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading;
using System;
using Models.SearchCriterias;

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

        public override IEnumerable<Order> Get()
        {
            // Eadger Loading (zachłanne ładowanie)
            // https://docs.microsoft.com/pl-pl/ef/ef6/querying/related-data#eagerly-loading-multiple-levels

            return entities
                // .Include(nameof(Customer))
                .Include(p => p.Customer) // using System.Data.Entity
                .Include(p => p.Details.Select(x => x.Item))                
                .ToList();

            // EF Core
            // ThenInclude
        }

        // Lazy Loading
        //public IEnumerable<Order> Calculate()
        //{
        //    // Lazy Loading
        //    1. public virtual
        //    2. context.Configuration.ProxyCreationEnabled = true;
        //    3. context.Configuration.LazyLoadingEnabled = true;

        //    var orders = entities.ToList();

        //    foreach (var order in orders)
        //    {
        //        System.Console.WriteLine(order.Customer.FullName);

        //        Thread.Sleep(TimeSpan.FromSeconds(5));

        //    }

        //    return orders;
        //}

        // Explicit Loading (jawne ładowanie)
        public IEnumerable<Order> Calculate()
        {
            context.Configuration.ProxyCreationEnabled = false;
            context.Configuration.LazyLoadingEnabled = false;

            var orders = entities.ToList();

            foreach (var order in orders)
            {
                context.Entry(order).Reference(p => p.Customer).Load(); // Load object

                // context.Entry(order).Collection(p => p.Details).Load(); // Load collection

                // Filtrowanie dociąganych danych
                context.Entry(order).Collection(p => p.Details).Query().Where(d => d.Quantity > 5).Load();

                System.Console.WriteLine(order.Customer.FullName);

                // Thread.Sleep(TimeSpan.FromSeconds(5));

            }

            return orders;
        }

        public IEnumerable<Order> Get2(OrderSearchCriteria searchCriteria)
        {
            var query = from p in context.Orders
                            where p.Status == searchCriteria.Status
                            orderby p.OrderDate, p.OrderNumber
                            select p;

            return query.ToList();
        }

        public IEnumerable<Order> Get(OrderSearchCriteria searchCriteria)
        {
            // zła praktyka
            //return entities
            //    .Where(p => searchCriteria.From.HasValue && p.OrderDate >= searchCriteria.From && searchCriteria.To.HasValue && p.OrderDate <= searchCriteria.To)
            //    .Where(p => p.OrderNumber.StartsWith(searchCriteria.OrderNumber))
            //    .Where(p=> searchCriteria.Status.HasValue && p.Status == searchCriteria.Status)
            //    .ToList();


            IQueryable<Order> query = context.Orders.AsQueryable();

            if (searchCriteria.From.HasValue)
            {
                query = query.Where(p => p.OrderDate >= searchCriteria.From);
            }

            if (searchCriteria.To.HasValue)
            {
                query = query.Where(p => p.OrderDate <= searchCriteria.To);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.OrderNumber))
            {
                query = query.Where(p => p.OrderNumber.StartsWith(searchCriteria.OrderNumber));
            }

            if (searchCriteria.Status.HasValue)
            {
                query = query.Where(p => p.Status == searchCriteria.Status);
            }

            // uwaga: błędne!
            // query = query.OrderBy(p => p.OrderDate).OrderBy(p => p.OrderNumber);

            query = query.OrderBy(p => p.OrderDate).ThenBy(p=>p.OrderNumber);

            return query.ToList();


        }

        public IEnumerable<Order> GetActive()
        {
            var activeCustomers = context.Customers.Where(c=>!c.IsRemoved).ToList();

            var woman = context.Customers.Where(c => c.Gender == Gender.Female);

            var man = context.Customers.Where(c => c.Gender == Gender.Male);

            var query = activeCustomers.Except(woman).Union(man);
           
            throw new NotImplementedException();
        }
    }
}
