using Microsoft.EntityFrameworkCore;
using yuapi.Domain.InterfaceInfoAggregate;
using yuapi.Domain.MenuAggregate;
using yuapi.Domain.UserAggregate;

namespace yuapi.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<InterfaceInfo> InterfaceInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
