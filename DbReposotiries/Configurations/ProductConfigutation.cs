using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbReposotiries.Configurations
{
    public class ProductConfigutation : EntityTypeConfiguration<Product>
    {
        public ProductConfigutation()
        {
            this.Property(p => p.BarCode).HasMaxLength(13).IsFixedLength().IsUnicode(false);
        }
    }
}
