using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public static class PricesExtensions
    {
        public static IPrice CHF(this decimal amount)
        {
            return new Price(CurrenciesRepository.CHF, amount);
        }

        public static IPrice CHF(this int amount)
        {
            return new Price(CurrenciesRepository.CHF, amount);
        }
    }
}
