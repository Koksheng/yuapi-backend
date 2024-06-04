using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yuapi.Domain.UserAggregate;
using yuapi.Domain.UserAggregate.ValueObjects;

namespace yuapi.Infrastructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUsersTable(builder);
        }

        private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(m => m.Id);

            //builder.Property(m => m.Id)
            //       .HasConversion(
            //           id => id.Value,
            //           value => UserId.Create(value))
            //       .IsRequired();
            builder.Property(m => m.Id)
                   .HasConversion(
                       id => id.Value,
                       value => UserId.Create(value))
                   .ValueGeneratedOnAdd() // Ensure ID is generated on add
                   .IsRequired();

            builder.Property(m => m.userAccount).HasMaxLength(1000);
        }
    }
}
