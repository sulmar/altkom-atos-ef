using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbReposotiries.Extensions
{
    public static class DbContextExtensions
    {
        public static DbContext BulkInsert<T>(this DbContext context, T entity, int count, int batchSize)
            where T : class
        {
            context.Set<T>().Add(entity);

            if (count % batchSize == 0)
            {
                context.SaveChanges();

                Debug.WriteLine($"Saved {count} entities.");
            }
            return context;
        }
    }
}
