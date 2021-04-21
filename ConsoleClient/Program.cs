using DbReposotiries;
using Fakers;
using IRepositories;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            AddRangeCustomersTest();

            // AddCustomerTest();

            //AddOrderTest();

            // RemoveCustomerTest();

            // UpdateCustomerTest();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        private static void AddRangeCustomersTest()
        {
            CustomerFaker customerFaker = new CustomerFaker(new AddressFaker());

            var customers = customerFaker.Generate(100);

            string connectionString = ConfigurationManager.ConnectionStrings["ShopConnectionString"].ConnectionString;
            DbConnection connection = new SqlConnection(connectionString);

            ShopContext context = new ShopContext(connection, contextOwnsConnection: false);

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            customerRepository.AddRange(customers);

        }

        private static void AddOrderTest()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ShopConnectionString"].ConnectionString;
            DbConnection connection = new SqlConnection(connectionString);

            ShopContext context = new ShopContext(connection, contextOwnsConnection: false);

            ICustomerRepository customerRepository = new DbCustomerRepository(context);
            IOrderRepository orderRepository = new DbOrderRepository(context);

            Customer customer = customerRepository.Get(4);

            Order order = new Order
            {
                Customer = customer,
                OrderNumber = "ZAM 001/2021"                
            };

            Product product = new Product { Name = "EF6 in Action", BarCode = "54543534534", Color = "Black", UnitPrice = 199.99m };

            order.Details.Add(new OrderDetail { Item = product, Quantity = 5, UnitPrice = product.UnitPrice });

            orderRepository.Add(order);


        }

        private static void AddCustomerTest()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ShopConnectionString"].ConnectionString;
            DbConnection connection = new SqlConnection(connectionString);

            ShopContext context = new ShopContext(connection, contextOwnsConnection: false);

            Customer customer = new Customer
            {
                FirstName = "John",
                LastName = "Smith",
                Gender = Gender.Male,
                ShipAddress = new Address { City = "Warsaw", Country = "Poland", Street = "Towarowa", PostCode = "00-111" },
                InvoiceAddress = new Address { City = "Opole", Country = "Poland", Street = "Dworcowa", PostCode = "99-200" },
            };


            ICustomerRepository customerRepository = new DbCustomerRepository(context);
            customerRepository.Add(customer);
        }

        private static void RemoveCustomerTest()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ShopConnectionString"].ConnectionString;
            DbConnection connection = new SqlConnection(connectionString);

            ShopContext context = new ShopContext(connection, contextOwnsConnection: false);
            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            customerRepository.Remove(3);
        }

        private static void UpdateCustomerTest()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ShopConnectionString"].ConnectionString;
            DbConnection connection = new SqlConnection(connectionString);

            ShopContext context = new ShopContext(connection, contextOwnsConnection: false);
            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            Customer customer = new Customer { Id = 3, FirstName = "Jan", LastName = "Nowak" };

            customerRepository.Update(customer);
        }
    }
}
