namespace CurrencyExchangeService
{
    using CurrencyExchangeService.Core;
    using CurrencyExchangeService.Models;
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DBConnectString")
        {
            Database.SetInitializer<DataContext>(new DataBaseInitialized());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // CurrencyPair disable CascadeOnDelete
            modelBuilder.Entity<CurrencyPair>()
                        .HasOptional<Currency>(c => c.BaseCurrency)
                        .WithMany()
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<CurrencyPair>()
                        .HasOptional<Currency>(c => c.ChildCurrency)
                        .WithMany()
                        .WillCascadeOnDelete(false);
        }

        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CurrencyPair> CurrencyPair { get; set; }
        public virtual DbSet<RateHistory> RateHistory { get; set; }
    }
}