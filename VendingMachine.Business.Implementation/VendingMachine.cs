using System;
using System.Collections.Generic;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachine : IVendingMachine
    {
        public uint Capacity { get; private set; }

        protected readonly IVendingMachineStore store;
        protected readonly ICashRegister cashRegister;
        protected readonly IVendingMachineControlPanel controlPanel;

        public IVendingMachineStore Store => store;
        public ICashRegister CashRegister => cashRegister;
        public IVendingMachineControlPanel ControlPanel => controlPanel;

        public ICurrency Currency { get; private set; }
        public decimal InsertedAmount { get; private set; }

        public VendingMachine(IVendingMachineCatalog catalog, uint capacity)
        {
            store = new VendingMachineStore(catalog, capacity);
        }

        public IVendingMachine Feed(IEnumerable<IVendingMachineItem> items)
        {
            store.Store(items);

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

        public bool TryBuyItem(IVendingMachineProduct product)
        {
            throw new NotImplementedException();
        }
    }
}
