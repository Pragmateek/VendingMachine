using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachineStore : IVendingMachineStore
    {
        private readonly IList<IVendingMachineStoreSlot> slots = new List<IVendingMachineStoreSlot>();

        public VendingMachineStore(IVendingMachineCatalog catalog, uint capacity)
        {
            foreach (var catalogEntry in catalog)
            {
                var slot = new VendingMachineStoreSlot(catalogEntry, capacity);
                slots.Add(slot);
            }
        }

        public void Store(IEnumerable<IVendingMachineItem> items)
        {
            foreach (var item in items)
            {
                Store(item);
            }
        }

        public void Store(IVendingMachineItem item)
        {
            var itemProductSlot = slots.SingleOrDefault(slot => slot.CatalogEntry.Product == item.Product);

            if (itemProductSlot == null)
            {
                throw new Exception($"This store can't handle '{item.Product}'!");
            }

            itemProductSlot.Store(item);
        }

        public IEnumerator<IVendingMachineStoreSlot> GetEnumerator()
        {
            return slots.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return slots.GetEnumerator();
        }
    }
}
