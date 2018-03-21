using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachineStoreSlot : IVendingMachineStoreSlot
    {
        private readonly Queue<IVendingMachineItem> items = new Queue<IVendingMachineItem>();

        public event EventHandler ItemsChanged = delegate { };

        public IVendingMachineCatalogEntry CatalogEntry { get; private set; }
        public uint Capacity { get; private set; }
        public uint Count => (uint)items.Count;

        public VendingMachineStoreSlot(IVendingMachineCatalogEntry catalogEntry, uint capacity)
        {
            CatalogEntry = catalogEntry;
            Capacity = capacity;
        }

        public void Feed(IEnumerable<IVendingMachineItem> newItems)
        {
            var firstWrongProductItem = newItems.FirstOrDefault(newItem => newItem.Product != CatalogEntry.Product);

            if (firstWrongProductItem != null)
            {
                throw new Exception($"This slot can only store '{CatalogEntry.Product}', not '{firstWrongProductItem.Product}'!");
            }

            foreach (var newItem in newItems)
            {
                items.Enqueue(newItem);
            }

            ItemsChanged(this, EventArgs.Empty);
        }

        public void Take(IVendingMachineItem product)
        {

        }

        public IEnumerator<IVendingMachineItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
