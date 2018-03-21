using System.Collections;
using System.Collections.Generic;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachineCatalog : IVendingMachineCatalog
    {
        private IList<IVendingMachineCatalogEntry> entries = new List<IVendingMachineCatalogEntry>();

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
