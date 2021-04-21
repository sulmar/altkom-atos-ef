using IRepositories;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbReposotiries
{
    public class DbProductRepository : DbEntityRepository<Product>, IProductRepository
    {
        public DbProductRepository(ShopContext context) : base(context)
        {
        }

        public override IEnumerable<Product> Get()
        {
            string sql = "select Id, Name, Color, Weight, BarCode from dbo.Items where Discriminator = 'Product' ";

            return context.Products.SqlQuery(sql).ToList();            
        }

        public override Product Get(int id)
        {
            // SQL Injection
            // string sql = "select Id, Name, Color, Weight, BarCode from dbo.Items where Discriminator = 'Product' AND Id = " + id;

            string sql = "select Id, Name, Color, Weight, BarCode from dbo.Items where Discriminator = 'Product' AND Id = @ProductId";

            var idParameter = new SqlParameter("@ProductId", id);

            return context.Products.SqlQuery(sql, idParameter).SingleOrDefault();
        }

        public Product GetByBarCode(string barCode)
        {
            // SQL Injection
            // string sql = "select Id, Name, Color, Weight, BarCode, UnitPrice from dbo.Items where Discriminator = 'Product' AND BarCode =  " + barCode;

             string sql = "select Id, Name, Color, Weight, BarCode, UnitPrice from dbo.Items where Discriminator = 'Product' AND BarCode = @BarCode";


            // Stored Procedure
            // string sql = "dbo.GetProductsByBarCode @BarCode";

            var barCodeParameter = new SqlParameter("@BarCode", barCode);

            return context.Products.SqlQuery(sql, barCodeParameter).SingleOrDefault();
        }


        public IEnumerable<Product> GetByColor(string color)
        {
            // SQL Injection
             // string sql = "select Id, Name, Color, Weight, BarCode, UnitPrice from dbo.Items where Discriminator = 'Product' AND Color =  '" + color + "'";

            string sql = "select Id, Name, Color, Weight, BarCode, UnitPrice from dbo.Items where Discriminator = 'Product' AND Color = @Color ";

            var colorParameter = new SqlParameter("@Color", color);

            return context.Products.SqlQuery(sql, colorParameter).ToList();
        }

        public override void Add(Product entity)
        {
            // string sql = "insert into dbo.Items values (@Name, @Color, @Weight, @BarCode)";

            string sql = "dbo.AddProduct @Name, @Color, @Weight, @BarCode";

            int rowsAffected = context.Database.ExecuteSqlCommand(sql, new SqlParameter("@Name", entity.Name), new SqlParameter("@Color", entity.Color));

            if (rowsAffected==0)
            {
                throw new ApplicationException("Błąd podczas dodawania produktu");
            }
        }
    }
}
