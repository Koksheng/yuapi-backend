using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using yuapi.Domain.InterfaceInfoAggregate.ValueObjects;
using yuapi.Domain.MenuAggregate;

namespace yuapi.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //public DbSet<Domain.Entities.InterfaceInfo> InterfaceInfo { get; set; }
        public DbSet<Domain.Entities.User> Users { get; set; }
        //public DbSet<Domain.InterfaceInfoAggregate.InterfaceInfo> InterfaceInfos { get; set; }
        public DbSet<Menu> Menus { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    var interfaceInfoIdConverter = new ValueConverter<InterfaceInfoId, int>(
        //        id => id.Value,
        //        value => InterfaceInfoId.Create(value));

        //    modelBuilder.Entity<Domain.InterfaceInfoAggregate.InterfaceInfo>(entity =>
        //    {
        //        entity.HasKey(e => e.Id);
        //        entity.Property(e => e.Id)
        //             .HasConversion(interfaceInfoIdConverter)
        //             .IsRequired();

        //        entity.Property(e => e.name).IsRequired();
        //        entity.Property(e => e.description).IsRequired();
        //        entity.Property(e => e.url).IsRequired();
        //        entity.Property(e => e.requestHeader).IsRequired();
        //        entity.Property(e => e.responseHeader).IsRequired();
        //        entity.Property(e => e.userId).IsRequired();
        //        entity.Property(e => e.status).IsRequired();
        //        entity.Property(e => e.method).IsRequired();
        //        entity.Property(e => e.createTime).IsRequired();
        //        entity.Property(e => e.updateTime).IsRequired();
        //        entity.Property(e => e.isDelete).IsRequired();

        //    });
        //}
    }
}
