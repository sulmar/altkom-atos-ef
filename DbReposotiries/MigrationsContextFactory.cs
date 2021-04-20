using System.Data.Entity.Infrastructure;

namespace DbReposotiries
{
    public class MigrationsContextFactory : IDbContextFactory<ShopContext>
    {
        public ShopContext Create()
        {
            return new ShopContext("ShopConnectionString");
        }
    }
}
