using CurrencyExchangeService.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace CurrencyExchangeService.Core
{
    public sealed class DataBaseInitialized: CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var currencyRUB = new Currency() { Name = "Russian Ruble", Code = "RUB" };
            var currencyEUR = new Currency() { Name = "Euro", Code = "EUR" };
            var currencyUSD = new Currency() { Name = "United States Dollar", Code = "USD" };
            context.Currencies.AddRange(new List<Currency>() { currencyRUB, currencyEUR, currencyUSD });
            context.SaveChanges();
            var defaultCurrencyPair = new List<CurrencyPair>();
            defaultCurrencyPair.Add(new CurrencyPair() { Name = "USD-RUB", BaseCurrency = currencyUSD, ChildCurrency = currencyRUB, BaseCurrencyID = currencyUSD.ID, ChildCurrencyID = currencyRUB.ID });
            defaultCurrencyPair.Add(new CurrencyPair() { Name = "EUR-RUB", BaseCurrency = currencyEUR, ChildCurrency = currencyRUB, BaseCurrencyID = currencyEUR.ID, ChildCurrencyID = currencyRUB.ID });
            context.CurrencyPair.AddRange(defaultCurrencyPair);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
