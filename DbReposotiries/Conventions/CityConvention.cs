using System.Data.Entity.ModelConfiguration.Conventions;

namespace DbReposotiries.Conventions
{
    public class CityConvention : Convention
    {
        public CityConvention()
        {
            this.Properties<string>().Where(p => p.Name.Contains("City"))
                .Configure(c => c.HasMaxLength(100));
        }
    }
}
