using VendingMachine.Data;

namespace VendingMachine.Business.Implementation
{
    public class LombardOdierVendingMachine : VendingMachine
    {
        public LombardOdierVendingMachine() :
            base(VendingMachinesCatalogsRepository.GetLombardOdierCatalog(), 10)
        {
        }
    }
}
