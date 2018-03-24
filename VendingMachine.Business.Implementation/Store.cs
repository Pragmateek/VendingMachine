using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;
using VendingMachine.Tools;

namespace VendingMachine.Business.Implementation
{
    public class Store : IStore
    {
        private readonly IList<IStoreSlot> slots = new List<IStoreSlot>();
        public IEnumerable<IStoreSlot> Slots => slots;

        public Store(ICatalog catalog, uint capacity)
        {
            foreach (var catalogEntry in catalog)
            {
                var slot = new StoreSlot(catalogEntry, capacity);
                slots.Add(slot);
            }
        }

        public void Put(IEnumerable<IItem> newItems)
        {
            foreach (var newItem in newItems)
            {
                Put(newItem);
            }
        }

        public void Put(IItem newItem)
        {
            var itemProductSlot = slots.SingleOrDefault(slot => slot.CatalogEntry.Product == newItem.Product);

            if (itemProductSlot == null)
            {
                throw new Exception($"This store can't handle '{newItem.Product}'!");
            }

            itemProductSlot.Store(newItem);
        }

        //public IEnumerator<IStoreSlot> GetEnumerator()
        //{
        //    return slots.GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return slots.GetEnumerator();
        //}

        public override bool Equals(object obj)
        {
            var otherStore = obj as Store;

            return otherStore != null && otherStore.Slots.SequenceEqual(Slots);
        }

        public override int GetHashCode()
        {
            return slots.GetElementsHashCode();
        }
    }
}
