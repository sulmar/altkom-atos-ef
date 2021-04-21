using IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DbReposotiries
{
    public class DbEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : class
    {
        protected readonly ShopContext context;

        protected DbSet<TEntity> entities => context.Set<TEntity>();

        public DbEntityRepository(ShopContext context)
        {
            this.context = context;
        }

        public virtual void Add(TEntity entity)
        {
            Console.WriteLine(context.Entry(entity).State);

            entities.Add(entity);

            var entries = context.ChangeTracker.Entries();

            foreach (DbEntityEntry entry in entries)
            {
                
            }

            Console.WriteLine(context.Entry(entity).State);

            context.SaveChanges();

            Console.WriteLine(context.Entry(entity).State);
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return entities.ToList();
        }

        public virtual TEntity Get(int id)
        {
            return entities.Find(id);
        }

        public virtual void Remove(int id)
        {
            entities.Remove(Get(id));
            context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            entities.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
