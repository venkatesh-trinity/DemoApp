using DemoApp.Domain.Entities;
using DemoApp.Domain.Events;
using DemoApp.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Infrastructure.Persistance
{
    public class AppDbContext: DbContext
	{
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

        public AppDbContext(DbContextOptions<AppDbContext> options,
            PublishDomainEventsInterceptor publishDomainEventsInterceptor)
            : base(options)
        {
            _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
            Database.EnsureCreated();
        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<List<IDomainEvent>>();

            modelBuilder.Entity<BankAccount>().HasKey(p => p.Id);
            modelBuilder.Entity<BankAccount>().HasData(
                BankAccount.Create("ICICI123456", "Savings", "test", "test bank"),
                BankAccount.Create("ICICI023456", "Savings", "test", "test bank"),
                BankAccount.Create("ICICICU123456", "Current", "test", "test bank")
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

