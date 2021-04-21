using DbReposotiries;
using Fakers;
using IRepositories;
using Models;
using Models.SearchCriterias;
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

            GetOrderSearchCriteriaTest();

            // CalculateOrdersTest();

            // GetOrdersTest();

            // AddRangeOrdersTest();

            //   AddRangeCustomersTest();

            // AddCustomerTest();

            //AddOrderTest();

            // RemoveCustomerTest();

            // UpdateCustomerTest();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        private static void GetOrderSearchCriteriaTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();

            OrderSearchCriteria searchCriteria = new OrderSearchCriteria
            {
                From = DateTime.Parse("2021-01-01"),
                To = DateTime.Parse("2021-05-01"),
            };

            IOrderRepository orderRepository = new DbOrderRepository(context);

            var orders = orderRepository.Get(searchCriteria);

        }

        private static void GetOrdersTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();

            IOrderRepository orderRepository = new DbOrderRepository(context);
            var orders = orderRepository.Get();
        }

        private static void CalculateOrdersTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();

            IOrderRepository orderRepository = new DbOrderRepository(context);
            var orders = orderRepository.Calculate();
        }

        private static void AddRangeOrdersTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            var customers = customerRepository.Get();

            ProductFaker productFaker = new ProductFaker();
            var products = productFaker.Generate(50);

            ServiceFaker serviceFaker = new ServiceFaker();
            var services = serviceFaker.Generate(10);

            // Union - suma zbiorów
            var items = products.OfType<Item>().Union(services);

            OrderFaker orderFaker = new OrderFaker(customers, new OrderDetailFaker(items));

            var orders = orderFaker.Generate(10);

            IOrderRepository orderRepository = new DbOrderRepository(context);
            orderRepository.AddRange(orders);
        }

        private static void AddRangeCustomersTest()
        {
            CustomerFaker customerFaker = new CustomerFaker(new AddressFaker());

            var customers = customerFaker.Generate(100);

            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            customerRepository.AddRange(customers);

        }

        private static void AddOrderTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();

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
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();

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
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            customerRepository.Remove(3);
        }

        private static void UpdateCustomerTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            Customer customer = new Customer { Id = 3, FirstName = "Jan", LastName = "Nowak" };

            customerRepository.Update(customer);
        }
    }
}
