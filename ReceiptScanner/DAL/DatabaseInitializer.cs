using ReceiptScanner.Models.EntityModels;
using System.Collections.Generic;

namespace ReceiptScanner.DAL
{
    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            //var currencies = new List<Currency>
            //{
            //    new Currency
            //    {
            //        Code = "DKK",
            //        Symbol = "kr",
            //        ExchangeRate = 74.47750
            //    },
            //    new Currency
            //    {
            //        Code = "ALL",
            //        Symbol = "L",
            //        ExchangeRate = 129.380
            //    },
            //    new Currency
            //    {
            //        Code = "AUD",
            //        Symbol = "$",
            //        ExchangeRate = 1.58692
            //    },
            //    new Currency
            //    {
            //        Code = "AZN",
            //        Symbol = "₼",
            //        ExchangeRate = 2.10234
            //    },
            //    new Currency
            //    {
            //        Code = "BAM",
            //        Symbol = "KM",
            //        ExchangeRate = 1.95583
            //    },
            //    new Currency
            //    {
            //        Code = "BGN",
            //        Symbol = "лв",
            //        ExchangeRate = 1.95583
            //    },
            //    new Currency
            //    {
            //        Code = "BYN",
            //        Symbol = "Br",
            //        ExchangeRate = 2.49513
            //    },
            //    new Currency
            //    {
            //        Code = "CHF",
            //        Symbol = "CHF",
            //        ExchangeRate = 1.19771
            //    },
            //    new Currency
            //    {
            //        Code = "CZK",
            //        Symbol = "Kč",
            //        ExchangeRate = 25.3263
            //    },
            //    new Currency
            //    {
            //        Code = "EUR",
            //        Symbol = "€",
            //        ExchangeRate = 1
            //    },
            //    new Currency
            //    {
            //        Code = "GBP",
            //        Symbol = "£",
            //        ExchangeRate = 0.869067
            //    },
            //    new Currency
            //    {
            //        Code = "GEL",
            //        Symbol = "₾",
            //        ExchangeRate = 3.00487
            //    },
            //    new Currency
            //    {
            //        Code = "HRK",
            //        Symbol = "kn",
            //        ExchangeRate = 7.41328
            //    },
            //    new Currency
            //    {
            //        Code = "HUF",
            //        Symbol = "Ft",
            //        ExchangeRate = 310.415
            //    },
            //    new Currency
            //    {
            //        Code = "ISK",
            //        Symbol = "kr",
            //        ExchangeRate = 123.315
            //    },
            //    new Currency
            //    {
            //        Code = "MDL",
            //        Symbol = "L",
            //        ExchangeRate = 20.3171
            //    },
            //    new Currency
            //    {
            //        Code = "MKD",
            //        Symbol = "ден",
            //        ExchangeRate = 61.4200
            //    },
            //    new Currency
            //    {
            //        Code = "NOK",
            //        Symbol = "kr",
            //        ExchangeRate = 9.57955
            //    },
            //    new Currency
            //    {
            //        Code = "PLN",
            //        Symbol = "zł",
            //        ExchangeRate = 4.16847
            //    },
            //    new Currency
            //    {
            //        Code = "RSD",
            //        Symbol = "RSD",
            //        ExchangeRate = 118.167
            //    },
            //    new Currency
            //    {
            //        Code = "RUB",
            //        Symbol = "₽",
            //        ExchangeRate = 75.4110
            //    },
            //    new Currency
            //    {
            //        Code = "SEK",
            //        Symbol = "kr",
            //        ExchangeRate = 10.3691
            //    },
            //    new Currency
            //    {
            //        Code = "TRY",
            //        Symbol = "₺",
            //        ExchangeRate = 4.98855
            //    },
            //    new Currency
            //    {
            //        Code = "UAH",
            //        Symbol = "₴",
            //        ExchangeRate = 32.5470
            //    },
            //    new Currency
            //    {
            //        Code = "USD",
            //        Symbol = "$",
            //        ExchangeRate = 1.23747
            //    }
            //};

            //currencies.ForEach(c => context.Currencies.Add(c));

            //var countries = new List<Country>
            //{
            //    new Country
            //    {
            //        Code = "DK",
            //        Name = "Denmark",
            //        Currency_Code = "DKK"
            //    }
            //};

            //countries.ForEach(c => context.Countries.Add(c));

            //context.SaveChanges();
        }
    }
}