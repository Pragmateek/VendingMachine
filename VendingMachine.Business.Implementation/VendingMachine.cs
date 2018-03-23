using System;
using System.Collections.Generic;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachine : IVendingMachine
    {
        public uint Capacity { get; private set; }

        protected readonly IStore store;
        protected readonly ICashRegister cashRegister;
        protected readonly IControlPanel controlPanel;

        public IStore Store => store;
        public ICashRegister CashRegister => cashRegister;
        public IControlPanel ControlPanel => controlPanel;

        public ICurrency Currency { get; private set; }
        public decimal InsertedAmount { get; private set; }

        public VendingMachine(ICatalog catalog, uint storeSlotsCapacity, IEnumerable<ICoinType> acceptedCoinsTypes, uint cashRegisterCapacity)
        {
            store = new Store(catalog, storeSlotsCapacity);
            cashRegister = new CashRegister(acceptedCoinsTypes, cashRegisterCapacity);
            controlPanel = new ControlPanel(catalog, acceptedCoinsTypes);
        }

        public IVendingMachine Feed(IEnumerable<IItem> items)
        {
            store.Put(items);

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

        public bool TryBuyItem(IProduct product)
        {
            throw new NotImplementedException();
        }
    }
}
