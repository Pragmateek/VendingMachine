using VendingMachine.Business;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class CurrenciesRepository
    {
        public static readonly ICurrency CHF = new Currency("CHF");
    }
}
