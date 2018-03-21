using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class VendingMachinesCatalogsRepository
    {
        public static IVendingMachineCatalog GetLombardOdierCatalog()
        {
            var EvianPrice = new Price(CurrenciesRepository.CHF, 1.10m);
            var CocaColaPrice = new Price(CurrenciesRepository.CHF, 1.20m);
            var FantaPrice = new Price(CurrenciesRepository.CHF, 1.60m);

            var catalog = new VendingMachineCatalog();
            catalog.ReferenceProduct(VendingMachineProductsRepository.Evian, EvianPrice);
            catalog.ReferenceProduct(VendingMachineProductsRepository.CocaCola, CocaColaPrice);
            catalog.ReferenceProduct(VendingMachineProductsRepository.Fanta, FantaPrice);

            return catalog;
        }
    }
}
