using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachineCatalogEntry : IVendingMachineCatalogEntry
    {
        public IVendingMachineProduct Product { get; private set; }

        public IPrice Price { get; private set; }

        public VendingMachineCatalogEntry(IVendingMachineProduct product, IPrice price)
        {
            Product = product;
            Price = price;
        }
    }
}
