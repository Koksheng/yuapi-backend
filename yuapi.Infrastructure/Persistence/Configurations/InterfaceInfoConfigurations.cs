using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Domain.InterfaceInfoAggregate;
using yuapi.Domain.UserAggregate.ValueObjects;
using yuapi.Domain.UserAggregate;
using yuapi.Domain.InterfaceInfoAggregate.ValueObjects;

namespace yuapi.Infrastructure.Persistence.Configurations
{
    public class InterfaceInfoConfigurations : IEntityTypeConfiguration<InterfaceInfo>
    {
        public void Configure(EntityTypeBuilder<InterfaceInfo> builder)
        {
            ConfigureInterfaceInfosTable(builder);
        }
        private void ConfigureInterfaceInfosTable(EntityTypeBuilder<InterfaceInfo> builder)
        {
            builder.ToTable("InterfaceInfos");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                   .HasConversion(
                       id => id.Value,
                       value => InterfaceInfoId.Create(value))
                   .ValueGeneratedOnAdd() // Ensure ID is generated on add
                   .IsRequired();

            //builder.Property(m => m.name).HasMaxLength(1000);
        }
    }
}
