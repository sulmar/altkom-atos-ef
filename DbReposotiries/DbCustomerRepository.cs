﻿using IRepositories;
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
            Console.WriteLine(context.Entry(customer).State);

            context.Customers.Add(customer); // <- dodanie obiektu do Contextu

            Console.WriteLine(context.Entry(customer).State);

            context.SaveChanges();           // <- wysłanie zapytania SQL do bazy danych

            Console.WriteLine(context.Entry(customer).State);
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

        public void Remove(int id)
        {
            //Customer customer = Get(id);

            //Console.WriteLine(context.Entry(customer).State);

            //context.Customers.Remove(customer);

            //Console.WriteLine(context.Entry(customer).State);

            //context.SaveChanges();

            //Console.WriteLine(context.Entry(customer).State);

            Customer customer = new Customer { Id = id };

            Console.WriteLine(context.Entry(customer).State);

            context.Customers.Attach(customer);

            Console.WriteLine(context.Entry(customer).State);

            context.Entry(customer).State = System.Data.Entity.EntityState.Deleted;

            Console.WriteLine(context.Entry(customer).State);

            context.SaveChanges();
            Console.WriteLine(context.Entry(customer).State);
        }

        public void Update(Customer customer)
        {
            Console.WriteLine(context.Entry(customer).State);

            // EF Core
            // context.Customers.Update(customer);

            context.Customers.Attach(customer);

            Console.WriteLine(context.Entry(customer).State);

            context.Entry(customer).State = System.Data.Entity.EntityState.Modified;

            Console.WriteLine(context.Entry(customer).State);

            context.SaveChanges();

            Console.WriteLine(context.Entry(customer).State);

            //Customer existingCustomer = Get(customer.Id);

            //Console.WriteLine(context.Entry(existingCustomer).State);

            //existingCustomer.FirstName = customer.FirstName;

            //Console.WriteLine(context.Entry(existingCustomer).State);

            //existingCustomer.LastName = existingCustomer.LastName;

            //Console.WriteLine(context.Entry(existingCustomer).State);

            //context.SaveChanges();

            //Console.WriteLine(context.Entry(existingCustomer).State);


        }
    }
}
