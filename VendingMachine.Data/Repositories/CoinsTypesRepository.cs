using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class CoinsTypesRepository
    {
        public static readonly ICoinType FiveSwissFrancs = new CoinType("5 Swiss Francs", CurrenciesRepository.CHF, 5);
        public static readonly ICoinType TwoSwissFrancs = new CoinType("2 Swiss Francs", CurrenciesRepository.CHF, 2);
        public static readonly ICoinType OneSwissFranc = new CoinType("1 Swiss Franc", CurrenciesRepository.CHF, 1);
        public static readonly ICoinType FiftySwissFrancCents = new CoinType("50 Swiss Franc Cents", CurrenciesRepository.CHF, 0.50m);
        public static readonly ICoinType TwentySwissFrancCents = new CoinType("20 Swiss Franc Cents", CurrenciesRepository.CHF, 0.20m);
        public static readonly ICoinType TenSwissFrancCents = new CoinType("10 Swiss Franc Cents", CurrenciesRepository.CHF, 0.10m);
        public static readonly ICoinType FiveSwissFrancCents = new CoinType("5 Swiss Franc Cents", CurrenciesRepository.CHF, 0.05m);

        public static readonly IEnumerable<ICoinType> SwissFrancCoins = new[] { FiveSwissFrancs, TwoSwissFrancs, OneSwissFranc, FiftySwissFrancCents, TwentySwissFrancCents, TenSwissFrancCents, FiveSwissFrancCents };

        public static readonly IEnumerable<ICoinType> AllCoinTypes = SwissFrancCoins;

        public static ICoinType GetCoinTypeByName(string coinTypeName)
        {
            return AllCoinTypes.FirstOrDefault(coinType => coinType.Name == coinTypeName);
        }
    }
}
