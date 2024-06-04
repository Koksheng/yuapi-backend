using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Domain.Common.Models;
using yuapi.Domain.MenuAggregate;
using yuapi.Domain.MenuAggregate.ValueObjects;

namespace yuapi.Infrastructure.Persistence.Configurations
{
    public class MenuConfigurations : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            ConfigureMenusTable(builder);
        }

        private void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .HasConversion(
                       id => id.Value,
                       value => MenuId.Create(value))
                   .IsRequired();

            builder.Property(m => m.Name).HasMaxLength(1000);
            //builder.Property(m => m.Description).HasMaxLength(1000);
            //builder.Property(m => m.Url).HasMaxLength(1000);
            //builder.Property(m => m.RequestHeader).HasMaxLength(1000);
            //builder.Property(m => m.ResponseHeader).HasMaxLength(1000);
            //builder.Property(m => m.UserId).IsRequired();
            //builder.Property(m => m.Status).IsRequired();
            //builder.Property(m => m.Method).IsRequired();
            //builder.Property(m => m.CreateTime).IsRequired();
            //builder.Property(m => m.UpdateTime).IsRequired();
            //builder.Property(m => m.IsDelete).IsRequired();
        }
    }
}
