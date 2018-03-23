using VendingMachine.Data;

namespace VendingMachine.Business.Implementation
{
    public class LombardOdierVendingMachine : VendingMachine
    {
        public LombardOdierVendingMachine() :
            base(CatalogsRepository.GetLombardOdierCatalog(), 10, CoinsTypesRepository.SwissFrancCoins, 100)
        {
        }
    }
}
