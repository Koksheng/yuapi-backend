using Microsoft.EntityFrameworkCore;
using yuapi.Domain.Entities;

namespace yuapi.Application.Data
{
    public class DataContext : DbContext
    {
        //public DataContext(DbContextOptions<DataContext> options) : base(options) 
        //{
        //}

        //public DbSet<InterfaceInfo> InterfaceInfo { get; set; }
        //public DbSet<User> Users { get; set; }

        ////protected override void OnModelCreating(ModelBuilder modelBuilder)
        ////{
        ////    modelBuilder.Entity<User>()
        ////        .Property(u => u.Id)
        ////        .ValueGeneratedOnAdd(); // Assuming Id is an auto-generated primary key
        ////}
    }
}
