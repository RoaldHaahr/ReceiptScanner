using ReceiptScanner.Models.EntityModels;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace ReceiptScanner.Migrations
{
    public class Configuration : DbMigrationsConfiguration<DAL.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DAL.DatabaseContext context)
        {
            var currencies = new List<Currency>
            {
                new Currency
                {
                    Code = "DKK",
                    Symbol = "kr",
                    Exchange_Rate = 74.47750
                },
                new Currency
                {
                    Code = "ALL",
                    Symbol = "L",
                    Exchange_Rate = 129.380
                },
                new Currency
                {
                    Code = "AUD",
                    Symbol = "$",
                    Exchange_Rate = 1.58692
                },
                new Currency
                {
                    Code = "AZN",
                    Symbol = "₼",
                    Exchange_Rate = 2.10234
                },
                new Currency
                {
                    Code = "BAM",
                    Symbol = "KM",
                    Exchange_Rate = 1.95583
                },
                new Currency
                {
                    Code = "BGN",
                    Symbol = "лв",
                    Exchange_Rate = 1.95583
                },
                new Currency
                {
                    Code = "BYN",
                    Symbol = "Br",
                    Exchange_Rate = 2.49513
                },
                new Currency
                {
                    Code = "CHF",
                    Symbol = "CHF",
                    Exchange_Rate = 1.19771
                },
                new Currency
                {
                    Code = "CZK",
                    Symbol = "Kč",
                    Exchange_Rate = 25.3263
                },
                new Currency
                {
                    Code = "EUR",
                    Symbol = "€",
                    Exchange_Rate = 1
                },
                new Currency
                {
                    Code = "GBP",
                    Symbol = "£",
                    Exchange_Rate = 0.869067
                },
                new Currency
                {
                    Code = "GEL",
                    Symbol = "₾",
                    Exchange_Rate = 3.00487
                },
                new Currency
                {
                    Code = "HRK",
                    Symbol = "kn",
                    Exchange_Rate = 7.41328
                },
                new Currency
                {
                    Code = "HUF",
                    Symbol = "Ft",
                    Exchange_Rate = 310.415
                },
                new Currency
                {
                    Code = "ISK",
                    Symbol = "kr",
                    Exchange_Rate = 123.315
                },
                new Currency
                {
                    Code = "MDL",
                    Symbol = "L",
                    Exchange_Rate = 20.3171
                },
                new Currency
                {
                    Code = "MKD",
                    Symbol = "ден",
                    Exchange_Rate = 61.4200
                },
                new Currency
                {
                    Code = "NOK",
                    Symbol = "kr",
                    Exchange_Rate = 9.57955
                },
                new Currency
                {
                    Code = "PLN",
                    Symbol = "zł",
                    Exchange_Rate = 4.16847
                },
                new Currency
                {
                    Code = "RSD",
                    Symbol = "RSD",
                    Exchange_Rate = 118.167
                },
                new Currency
                {
                    Code = "RUB",
                    Symbol = "₽",
                    Exchange_Rate = 75.4110
                },
                new Currency
                {
                    Code = "SEK",
                    Symbol = "kr",
                    Exchange_Rate = 10.3691
                },
                new Currency
                {
                    Code = "TRY",
                    Symbol = "₺",
                    Exchange_Rate = 4.98855
                },
                new Currency
                {
                    Code = "UAH",
                    Symbol = "₴",
                    Exchange_Rate = 32.5470
                },
                new Currency
                {
                    Code = "USD",
                    Symbol = "$",
                    Exchange_Rate = 1.23747
                }
            };

            context.SaveChanges();

            currencies.ForEach(c => context.Currencies.AddOrUpdate(c));

            var countries = new List<Country>
            {
                new Country
                {
                    Code = "DK",
                    Name = "Denmark",
                    Currency_Code = "DKK"
                }
            };

            countries.ForEach(c => context.Countries.AddOrUpdate(c));

            context.SaveChanges();
        }

    }
}