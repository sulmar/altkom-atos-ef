using IRepositories;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbReposotiries
{
    public class DbServiceRepository : IServiceRepository
    {
        private readonly ShopContext context;

        public DbServiceRepository(ShopContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Service entity)
        {
            context.Services.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> GetAsync()
        {
            return await context.Services.ToListAsync();
        }

        public async Task<Service> GetAsync(int id)
        {
            return await context.Services.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            Service service = await GetAsync(id);
            context.Services.Remove(service);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Service entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
