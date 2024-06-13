using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yuapi.Domain.UserInterfaceInfoAggregate;
using yuapi.Domain.UserInterfaceInfoAggregate.ValueObjects;

namespace yuapi.Infrastructure.Persistence.Configurations
{
    public class UserInterfaceInfoConfigurations : IEntityTypeConfiguration<UserInterfaceInfo>
    {
        public void Configure(EntityTypeBuilder<UserInterfaceInfo> builder)
        {
            ConfigureUserInterfaceInfosTable(builder);
        }
        private void ConfigureUserInterfaceInfosTable(EntityTypeBuilder<UserInterfaceInfo> builder)
        {
            builder.ToTable("UserInterfaceInfos");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                   .HasConversion(
                       id => id.Value,
                       value => UserInterfaceInfoId.Create(value))
                   .ValueGeneratedOnAdd() // Ensure ID is generated on add
                   .IsRequired();
        }
    }
}
