using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class CurrenciesRepository
    {
        public static readonly ICurrency CHF = new Currency("CHF");

        public static readonly IEnumerable<ICurrency> AllCurrencies = new[] { CHF };

        public static ICurrency GetCurrencyByName(string currencyName)
        {
            return AllCurrencies.FirstOrDefault(currency => currency.Name == currencyName);
        }
    }
}
