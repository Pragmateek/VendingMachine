using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class StoreSlot : IStoreSlot
    {
        private readonly Queue<IItem> items = new Queue<IItem>();

        public event EventHandler ItemsChanged = delegate { };

        public ICatalogEntry CatalogEntry { get; private set; }
        public uint Capacity { get; private set; }
        public uint Count => (uint)items.Count;

        public StoreSlot(ICatalogEntry catalogEntry, uint capacity)
        {
            CatalogEntry = catalogEntry;
            Capacity = capacity;
        }

        public void Store(IItem newItem)
        {
            Store(new[] { newItem });
        }

        public void Store(IEnumerable<IItem> newItems)
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

        public IItem TakeOne()
        {
            if (items.Count == 0)
            {
                return null;
            }

            var item = items.Dequeue();

            ItemsChanged(this, EventArgs.Empty);

            return item;
        }

        public IEnumerator<IItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            var otherStoreSlot = obj as StoreSlot;

            return otherStoreSlot != null &&
                Equals(otherStoreSlot.CatalogEntry, CatalogEntry) &&
                otherStoreSlot.Capacity == Capacity &&
                otherStoreSlot.Count == Count;
        }

        public override int GetHashCode()
        {
            return CatalogEntry.GetHashCode() ^ Capacity.GetHashCode() ^ Count.GetHashCode();
        }

        public override string ToString()
        {
            return $"{CatalogEntry.Product}: {Count} / {Capacity}";
        }
    }
}
