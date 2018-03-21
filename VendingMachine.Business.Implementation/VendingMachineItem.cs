using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachineItem : IVendingMachineItem
    {
        public IVendingMachineProduct Product { get; private set; }

        public VendingMachineItem(IVendingMachineProduct product)
        {
            Product = product;
        }
    }
}
