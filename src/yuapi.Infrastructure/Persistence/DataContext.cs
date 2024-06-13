using Microsoft.EntityFrameworkCore;
using yuapi.Domain.Common.Models;
using yuapi.Domain.InterfaceInfoAggregate;
using yuapi.Domain.MenuAggregate;
using yuapi.Domain.UserAggregate;
using yuapi.Domain.UserInterfaceInfoAggregate;
using yuapi.Infrastructure.Persistence.Interceptors;

namespace yuapi.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
        public DataContext(DbContextOptions<DataContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
        {
            _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<InterfaceInfo> InterfaceInfos { get; set; }
        public DbSet<UserInterfaceInfo> UserInterfaceInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
