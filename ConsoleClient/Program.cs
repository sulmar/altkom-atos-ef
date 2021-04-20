using DbReposotiries;
using IRepositories;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
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

            AddCustomerTest();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        private static void AddCustomerTest()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ShopConnectionString"].ConnectionString;
            DbConnection connection = new SqlConnection(connectionString);
            ShopContext context = new ShopContext(connection, contextOwnsConnection: false);

            

            Customer customer = new Customer { FirstName = "John", LastName = "Smith", Gender = Gender.Male };

            ICustomerRepository customerRepository = new DbCustomerRepository(context);
            customerRepository.Add(customer);
        }
    }
}
