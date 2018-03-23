using System;
using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface IStoreSlot : IEnumerable<IItem>
    {
        ICatalogEntry CatalogEntry { get; }
        uint Capacity { get; }
        uint Count { get; }

        event EventHandler ItemsChanged;

        void Store(IItem newItem);
        void Store(IEnumerable<IItem> newItems);
    }
}
