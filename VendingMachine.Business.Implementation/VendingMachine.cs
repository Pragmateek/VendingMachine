using System;
using System.Collections.Generic;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachine : IVendingMachine
    {
        protected readonly ICatalog catalog;
        protected readonly IEnumerable<ICoinType> acceptedCoinsTypes;
        protected readonly IStore store;
        protected readonly ICashRegister cashRegister;
        protected readonly IControlPanel controlPanel;

        public ICatalog Catalog => catalog;
        public IEnumerable<ICoinType> AcceptedCoinsTypes => acceptedCoinsTypes;
        public IStore Store => store;
        public ICashRegister CashRegister => cashRegister;
        public IControlPanel ControlPanel => controlPanel;

        public ICurrency Currency { get; private set; }
        public decimal InsertedAmount { get; private set; }

        public VendingMachine(ICatalog catalog, uint storeSlotsCapacity, IEnumerable<ICoinType> acceptedCoinsTypes, uint cashRegisterCapacity)
        {
            this.catalog = catalog;
            this.acceptedCoinsTypes = acceptedCoinsTypes;
            store = new Store(catalog, storeSlotsCapacity);
            cashRegister = new CashRegister(acceptedCoinsTypes, cashRegisterCapacity);
            controlPanel = new ControlPanel(catalog, acceptedCoinsTypes, cashRegister);
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
