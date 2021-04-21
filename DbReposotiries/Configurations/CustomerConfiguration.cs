using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbReposotiries.Configurations
{

   
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            this.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
            this.Property(p => p.LastName).HasMaxLength(50).IsRequired();
            this.Property(p => p.Email).HasMaxLength(250);

            this.Property(p => p.Pesel).HasMaxLength(11).IsFixedLength().IsUnicode(false);

            this.HasIndex(p => p.Pesel).IsUnique();

            this.Ignore(p => p.IsSelected);
        }

        // EF Core
        //  public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
        //  { 
        // Configure(EntityTypeBuilder<TEntity> builder) 
        // 
        //public EntityTypeBuilder<TEntity> Configure(EntityTypeBuilder<TEntity> builder)
        //{
        //    builder.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
        //    builder.Property(p => p.LastName).HasMaxLength(50).IsRequired();
        //    builder.Property(p => p.Email).HasMaxLength(250);
        //    builder.Property(p => p.DateOfBirth).HasColumnType("datetime2");
        //}
    }
}
