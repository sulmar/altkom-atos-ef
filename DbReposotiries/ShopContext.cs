using DbReposotiries.Configurations;
using DbReposotiries.Conventions;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbReposotiries
{

    // PM> Install-Package EntityFramework
    public class ShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }


        public ShopContext(DbConnection connection, bool contextOwnsConnection)
            : this(new MigrateDatabaseToLatestVersion<ShopContext, DbReposotiries.Migrations.Configuration>(), connection, contextOwnsConnection)
        {

        }

        public ShopContext(IDatabaseInitializer<ShopContext> strategy, DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
        {
            Database.SetInitializer(strategy);

            // Database.SetInitializer(new CreateDatabaseIfNotExists<ShopContext>());

            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShopContext, DbReposotiries.Migrations.Configuration>());

            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ShopContext>());

            // Database.SetInitializer(new DropCreateDatabaseAlways<ShopContext>());
        }

        public ShopContext(string name)
            : base(name)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent Api
            // modelBuilder.Entity<Customer>().HasKey(p => p.Id);
            //modelBuilder.Entity<Customer>().Property(p => p.FirstName).HasMaxLength(50).IsRequired();
            //modelBuilder.Entity<Customer>().Property(p => p.LastName).HasMaxLength(50).IsRequired();
            //modelBuilder.Entity<Customer>().Property(p => p.Email).HasMaxLength(250);
            //modelBuilder.Entity<Customer>().Property(p => p.DateOfBirth).HasColumnType("datetime2");

            // modelBuilder.Entity<Order>().Property(p => p.OrderDate).HasColumnType("datetime2");

            //modelBuilder.Configurations.Add(new CustomerConfiguration());
            //modelBuilder.Configurations.Add(new OrderConfiguration());

            modelBuilder.Configurations.AddFromAssembly(typeof(BaseConfiguration).Assembly);

            // EF Core
            // modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            // modelBuilder.ApplyConfigurationFromAssembly(typeof(CustomerConfiguration).Assembly);

            modelBuilder.Conventions.Add(new DateTime2Convention());
            modelBuilder.Conventions.Add(new CityConvention());

            // modelBuilder.Conventions.AddFromAssembly(typeof(DateTime2Convention).Assembly);

        }


    }

    // Materializacja = recordset -> obiekty
}
