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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // SyncTest();

            // TaskTest();


            // SyncResultTest();

            // Task.Run(()=>AsyncAwaitTest());

            // GetServiceAsyncTest();


           // TaskResultTest();


            // UpdateCustomerTest();

            // RemoveCustomerTest();

            // GetProductByColorTest();

            // GetProductByBarCodeTest();

            // GetCustomersByGenderTest();

            // GetOrderSearchCriteriaTest();

            // CalculateOrdersTest();

            GetOrdersTest();

            // AddRangeOrdersTest();

            //   AddRangeCustomersTest();

            // AddCustomerTest();

            //AddOrderTest();



            // UpdateCustomerTest();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        private static async Task GetServiceAsyncTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();
            IServiceRepository serviceRepository = new DbServiceRepository(context);

            var services =  await serviceRepository.GetAsync();
        }

        private static void SyncResultTest()
        {
            int size1 = Download("http://www.google.com");
            Send($"Pobrano {size1}");

            int size2 = Download("http://www.microsoft.com");
            Send($"Pobrano {size2}");

            int size3 = Download("http://www.atos.net");
            Send($"Pobrano {size3}");
        }

        private static async Task AsyncAwaitTest()
        {
            int size1 = await DownloadAsync("http://www.google.com");            
            await SendAsync($"Pobrano {size1}");

            int size2 = await DownloadAsync("http://www.microsoft.com");
            await SendAsync($"Pobrano {size2}");

            int size3 = await DownloadAsync("http://www.atos.net");
            await SendAsync($"Pobrano {size3}");
        }

        private static void TaskContinueWithTest()
        {
            Task.Run(() => Download("http://www.google.com"))
                .ContinueWith(t => Send($"Pobrano {t.Result}"));

            Task.Run(() => Download("http://www.microsoft.com"))
                .ContinueWith(t => Send($"Pobrano {t.Result}"));

            Task.Run(() => Download("http://www.atos.net"))
                .ContinueWith(t => Send($"Pobrano {t.Result}"));
        }

       

        private static void TaskResultTest()
        {
            Task<int> t1 = Task<int>.Run(() => Download("http://www.google.com"));

            int size1 = t1.Result; // blokuje UI

            Task.Run(()=>Send($"Pobrano {size1}"));

            Task<int> t2 = Task<int>.Run(() => Download("http://www.microsoft.com"));

            int size2 = t2.Result;

            Task.Run(()=>Send($"Pobrano {size2}"));

            Task<int> t3 = Task<int>.Run(() => Download("http://www.atos.net"));

           int size3 = t3.Result;

            Task.Run(()=>Send($"Pobrano {size3}"));

        }

       
        private static Task<int> DownloadAsync(string url)
        {
            return Task.Run(() => Download(url));
        }

        private static int Download(string url)
        {
            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} downloading {url}...");

            WebClient client = new WebClient();
            string content = client.DownloadString(url);

            Thread.Sleep(TimeSpan.FromSeconds(3));

            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} downloaded.");

            return content.Length;
        }

        private static void SyncTest()
        {
            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} main");

            Send("Hello World!");

            Send("Hello EF!");

            Send("Hello .NET!");

            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} main");
        }

        private static void TaskTest()
        {
            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} main");

            for (int i = 0; i < 100; i++)
            {
                //Task task1 = new Task(() => Send("Hello World!"));
                //task1.Start();

                Task.Run(() => Send("Hello World!"));
            }
            
            //Task task2 = new Task(() => Send("Hello EF!"));
            //Task task3 = new Task(() => Send("Hello .NET!"));
            
            //task2.Start();
            //task3.Start();

            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} main");
        }

        private static Task SendAsync(string message)
        {
            return Task.Run(() => Send(message));
        }

        private static void Send(string message)
        {            
            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Sending {message}...");

            Thread.Sleep(TimeSpan.FromSeconds(5));

            Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Sent.");
        }

        private static void GetProductByBarCodeTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();

            IProductRepository productRepository = new DbProductRepository(context);

            string barcode = "3845437691226;DROP VIEW CustomersByGender";

            Product product = productRepository.GetByBarCode(barcode);

            Console.WriteLine(product.Name);
        }

        private static void GetProductByColorTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();

            IProductRepository productRepository = new DbProductRepository(context);

            string color = "red' OR 1=1 --";

            var products = productRepository.GetByColor(color);

            
        }

        private static void GetCustomersByGenderTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();


            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            var customersByGender = customerRepository.GetByGenders();
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

            if (orders.Any())
            {

            }
            else
            {
                Console.WriteLine("Brak zamówień");
            }


            if (orders.Any(c=>c.Customer.Gender == Gender.Female))
            {

            }

            if (orders.All(c=>c.Customer.Gender == Gender.Male))
            {

            }
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

            customerRepository.Remove(247);
        }

        private static void UpdateCustomerTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            ShopContext context = shopContextFactory.Create();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);


            Console.WriteLine("Type firstname: ");
            string firstname = Console.ReadLine();

            Customer customer = new Customer { Id = 225, FirstName = firstname, LastName = "Smith", Gender = Gender.Female };

            Console.WriteLine(customer.FirstName);

           
            customerRepository.Update(customer);
        }
    }
}
