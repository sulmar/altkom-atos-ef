using IRepositories;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
            Console.WriteLine(context.Entry(customer).State);

            context.Customers.Add(customer); // <- dodanie obiektu do Contextu

            Console.WriteLine(context.Entry(customer).State);

            context.SaveChanges();           // <- wysłanie zapytania SQL do bazy danych

            Console.WriteLine(context.Entry(customer).State);
        }

        public void AddRange(IEnumerable<Customer> customers)
        {
            context.Configuration.AutoDetectChangesEnabled = false;

            foreach (var customer in customers)
            {
                context.Customers.Add(customer);
            }

            context.ChangeTracker.DetectChanges();

            context.SaveChanges();

            context.Configuration.AutoDetectChangesEnabled = true;

            // context.Customers.AddRange(customers);
            // context.SaveChanges();
        }

        public IEnumerable<Customer> Get()
        {
            return context.Customers.ToList();
        }

        public Customer Get(int id)
        {
            return context.Customers.Find(id);

            // return context.Customers.SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<CustomerByGender> GetByGenders()
        {
            var query = context.Customers
                .GroupBy(c => c.Gender)
                .Select(g => new CustomerByGender { Gender = g.Key, Quantity = g.Count() });

            return query.ToList();
        }

        //public void Remove(int id)
        //{
        //    Customer customer = new Customer { Id = id, IsRemoved = true };

        //    context.Configuration.AutoDetectChangesEnabled = false;
        //    context.Configuration.ValidateOnSaveEnabled = false;

        //    context.Customers.Attach(customer);

        //    context.Entry(customer).Property(p => p.IsRemoved).IsModified = true;

        //    context.SaveChanges();

        //    context.Configuration.ValidateOnSaveEnabled = true;
        //    context.Configuration.AutoDetectChangesEnabled = true;
        //}




        public void Remove(int id)
        {
            RemoveTransactionScope(id);

            // RemoveTransactionNative(id);
        }



        // Transakcje rozproszone
        private void RemoveTransactionScope(int id)
        {
            Customer customer = new Customer { Id = id, IsRemoved = true };
            context.Configuration.ValidateOnSaveEnabled = false;

            try
            {
                // Add reference: System.Transaction
                using (TransactionScope scope = new TransactionScope())
                {
                    context.Customers.Attach(customer);

                    context.Entry(customer).Property(p => p.IsRemoved).IsModified = true;

                    context.SaveChanges();

                    var orders = context.Orders.Where(o => o.Customer.Id == customer.Id).ToList();

                    foreach (var order in orders)
                    {
                        order.Status = OrderStatus.Canceled;

                        throw new Exception();
                    }

                    context.SaveChanges();

                    scope.Complete(); // ustawiania flaga 
                } // Dispose - Commit // Rollback zależnie od flagi
            }
            catch (Exception)
            {

            }

        }

        // Transaction (natywne)
        /*
            declare @CustomerId int 
            set @CustomerId = 247

            begin tran

                update dbo.Customers  
                set IsRemoved = 1
                where Id = @CustomerId

                update dbo.Orders
                set Status = 2
                where Customer_Id = @CustomerId           

            commit
         */
        private void RemoveTransactionNative(int id)
        {
            Customer customer = new Customer { Id = id, IsRemoved = true };
            context.Configuration.ValidateOnSaveEnabled = false;


            // begin tran
            using (DbContextTransaction transaction = context.Database.BeginTransaction(isolationLevel: System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    // 1. update dbo.Customers  set IsRemoved = 1 where Id = @CustomerId

                    context.Customers.Attach(customer);

                    context.Entry(customer).Property(p => p.IsRemoved).IsModified = true;

                    context.SaveChanges();

                    // 2. update dbo.Orders set Status = 2 where Customer_Id = @CustomerId

                    var orders = context.Orders.Where(o => o.Customer.Id == customer.Id).ToList();

                    foreach (var order in orders)
                    {
                        order.Status = OrderStatus.Canceled;

                        throw new Exception();
                    }

                    context.SaveChanges();

                    // commit
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // rollback
                    transaction.Rollback();
                }
            }

            context.Configuration.ValidateOnSaveEnabled = true;

        }

        public void RemoveRange(IEnumerable<Customer> customers)
        {
            context.Customers.RemoveRange(customers);
            context.SaveChanges();
        }

        //public void Update(Customer customer)
        //{
        //    Console.WriteLine(context.Entry(customer).State);

        //    // EF Core
        //    // context.Customers.Update(customer);

        //    context.Customers.Attach(customer);

        //    Console.WriteLine(context.Entry(customer).State);

        //    context.Entry(customer).State = System.Data.Entity.EntityState.Modified;

        //    Console.WriteLine(context.Entry(customer).State);

        //    context.SaveChanges();

        //    Console.WriteLine(context.Entry(customer).State);


        //}

        public void Update(Customer customer)
        {
            Customer existingCustomer = Get(customer.Id);

            Console.WriteLine("Please Enter key to update");

            Console.ReadLine();


            existingCustomer.FirstName = customer.FirstName;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine("Ktoś już zmodyfikował tego klienta");
                
                // Pobranie encji z bazy danych
                e.Entries.Single().Reload();
            }
        }
    }
}
