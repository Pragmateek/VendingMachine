using System;
using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface IVendingMachineStoreSlot : IEnumerable<IVendingMachineItem>
    {
        IVendingMachineCatalogEntry CatalogEntry { get; }
        uint Capacity { get; }
        uint Count { get; }

        event EventHandler ItemsChanged;
    }
}
