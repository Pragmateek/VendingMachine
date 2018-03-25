using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Data
{
    public class CatalogsRepository
    {
        public static ICatalog GetLombardOdierCatalog()
        {
            var EvianPrice = new Price(CurrenciesRepository.CHF, 1.10m);
            var CocaColaPrice = new Price(CurrenciesRepository.CHF, 1.20m);
            var FantaPrice = new Price(CurrenciesRepository.CHF, 1.60m);

            var catalog = new Catalog();
            catalog.ReferenceProduct(ProductsRepository.EvianBottle, EvianPrice);
            catalog.ReferenceProduct(ProductsRepository.CocaColaBottle, CocaColaPrice);
            catalog.ReferenceProduct(ProductsRepository.FantaBottle, FantaPrice);

            return catalog;
        }
    }
}
