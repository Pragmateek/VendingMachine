using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class CurrenciesRepository
    {
        public static readonly ICurrency CHF = new Currency("CHF");
    }
}
