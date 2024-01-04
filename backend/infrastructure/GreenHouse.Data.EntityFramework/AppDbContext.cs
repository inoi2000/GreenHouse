using GreenHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GreenHouse.Data.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected AppDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Appartment>()
                .Property(a => a.Photos)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(new[] { ',' })
                .Cast<string>()
                .ToList());

        }

        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Appartment> Appartments => Set<Appartment>();
        public DbSet<RulesList> Rules => Set<RulesList>();
    }
}
