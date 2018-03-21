using System;
using System.Collections.Generic;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachine : IVendingMachine
    {
        public uint Capacity { get; private set; }

        protected readonly VendingMachineStore store;

        public IVendingMachineStore Store => store;

        public ICurrency Currency { get; private set; }
        public decimal InsertedAmount { get; private set; }

        public VendingMachine(IVendingMachineCatalog catalog, uint capacity)
        {
            store = new VendingMachineStore(catalog, capacity);
        }

        public IVendingMachine Feed(IEnumerable<IVendingMachineItem> items)
        {
            store.Add(items);

            return this;
        }

        public IVendingMachine Insert(ICoin coin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICoin> Refund()
        {
            throw new NotImplementedException();
        }

        public bool TryBuyItem(IVendingMachineItem item)
        {
            throw new NotImplementedException();
        }
    }
}
