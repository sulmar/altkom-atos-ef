using DbReposotiries.Configurations;
using DbReposotiries.Conventions;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
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
            ObjectContext.ObjectMaterialized += ObjectContext_ObjectMaterialized;
        }

        public ShopContext(IDatabaseInitializer<ShopContext> strategy, DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
        {
            Database.SetInitializer(strategy);


            // this.Configuration.AutoDetectChangesEnabled = false;

            // Database.SetInitializer(new CreateDatabaseIfNotExists<ShopContext>());

            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShopContext, DbReposotiries.Migrations.Configuration>());

            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ShopContext>());

            // Database.SetInitializer(new DropCreateDatabaseAlways<ShopContext>());
        }

        public ObjectContext ObjectContext => ((IObjectContextAdapter)this).ObjectContext;

        public ShopContext(string name)
            : base(name)
        {
           
        }

        private void ObjectContext_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            if (e.Entity is Order order)
            {
                order.OrderNumber = order.OrderNumber + "!";
            }
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

        public override int SaveChanges()
        {
            var addedEntities = this.ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added)
                .Select(e=>e.Entity);

            foreach (var entity in addedEntities)
            {
                entity.CreateOn = DateTime.Now;
            }            

            var modifiedEntities = this.ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Modified)
                .Select(e => e.Entity);

            var customers = ChangeTracker.Entries<Customer>()
                .Where(e => e.State == EntityState.Modified)
                .Select(e => e.Entity);


            foreach(var entity in modifiedEntities)
            {
                // https://docs.microsoft.com/pl-pl/ef/ef6/saving/change-tracking/property-values#reading-current-original-and-database-values-for-all-properties-of-an-entity

                Console.WriteLine("Current values:");
                PrintValues(this.Entry(entity).CurrentValues);

                Console.WriteLine("Original values:");
                PrintValues(this.Entry(entity).OriginalValues);

                Console.WriteLine("Database values:");
                PrintValues(this.Entry(entity).GetDatabaseValues());

                entity.ModifyOn = DateTime.Now;
            }

            return base.SaveChanges();
        }


        public static void PrintValues(DbPropertyValues values)
        {
            foreach (var propertyName in values.PropertyNames)
            {
                Console.WriteLine("Property {0} has value {1}",
                                  propertyName, values[propertyName]);
            }
        }




    }

    // Materializacja = recordset -> obiekty
}
