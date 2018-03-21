using System.Collections;
using System.Collections.Generic;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachineCatalog : IVendingMachineCatalog
    {
        private IList<IVendingMachineCatalogEntry> entries = new List<IVendingMachineCatalogEntry>();

        public void ReferenceProduct(IVendingMachineProduct product, IPrice price)
        {
            var newEntry = new VendingMachineCatalogEntry(product, price);

            entries.Add(newEntry);
        }

        public IEnumerator<IVendingMachineCatalogEntry> GetEnumerator()
        {
            return entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return entries.GetEnumerator();
        }
    }
}
