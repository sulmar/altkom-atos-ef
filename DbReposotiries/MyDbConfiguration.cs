using DbReposotiries.Interceptors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbReposotiries
{
    public class MyDbConfiguration : DbConfiguration
    {
        public MyDbConfiguration()
        {
            this.AddInterceptor(new DebugCommandInterceptor());
        }
    }
}
