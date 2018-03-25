using VendingMachine.Data;

namespace VendingMachine.Business.Implementation
{
    public class LombardOdierVendingMachine : VendingMachine
    {
        public LombardOdierVendingMachine(uint storeSlotsCapacity, uint cashRegisterSlotsCapacity) :
            base(CatalogsRepository.GetLombardOdierCatalog(), storeSlotsCapacity, CoinsTypesRepository.SwissFrancCoins, cashRegisterSlotsCapacity)
        {
        }
    }
}
