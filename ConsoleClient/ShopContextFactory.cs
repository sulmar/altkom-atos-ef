using DbReposotiries;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class ShopContextFactory : IDbContextFactory<ShopContext>
    {
        public ShopContext Create()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ShopConnectionString"].ConnectionString;
            DbConnection connection = new SqlConnection(connectionString);

            ShopContext context = new ShopContext(connection, contextOwnsConnection: false);

            context.Database.Log += Console.WriteLine; // nlog, serilog

            return context;

        }
    }
}
