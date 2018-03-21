using System;
using System.Collections.Generic;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachine : IVendingMachine
    {
        public uint Capacity { get; private set; }

        protected VendingMachineStore availableItems = new VendingMachineStore();

        public IVendingMachineStore AvailableItems => availableItems;

        public ICurrency Currency { get; private set; }
        public decimal InsertedAmount { get; private set; }

        public IVendingMachine Feed(IEnumerable<IVendingMachineItem> items)
        {
            return this;
        }

        public IVendingMachine Insert(ICoin coin)
        {
            throw new NotImplementedException();
        }

        public bool TryBuyItem(IVendingMachineItem item)
        {
            throw new NotImplementedException();
        }
    }
}
