using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbReposotiries.Configurations
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            this.Property(p => p.OrderDate).HasColumnType("datetime2");
            this.Property(p => p.OrderNumber).HasMaxLength(20).IsUnicode(false).IsRequired();
        }
    }
}
