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
                .Property(a => a.Conveniences)
                .HasConversion(
                    v => string.Join(",", v.Select(e => e.ToString("D")).ToArray()),
                    v => v.Split(new[] { ',' })
                .Select(e => Enum.Parse(typeof(Convenience), e))
                .Cast<Convenience>()
                .ToList());

            modelBuilder.Entity<Appartment>()
                .Property(a => a.Photos)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(new[] { ',' })
                .Cast<string>()
                .ToList());

        }

        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Appartment> Appartments => Set<Appartment>();
        public DbSet<RulesList> Rules => Set<RulesList>();
    }
}
