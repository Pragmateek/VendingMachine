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

        public void Feed(IEnumerable<IItem> items)
        {
            store.Put(items);
        }

        public void Insert(ICoin coin)
        {
            cashRegister.Put(coin);
        }

        public void Insert(IEnumerable<ICoin> coins)
        {
            cashRegister.Put(coins);
        }

        public IEnumerable<ICoin> Refund()
        {
            IEnumerable<ICoin> coins;

            cashRegister.TryGetChange(InsertedAmount, out coins);

            return coins;
        }

        public bool TryBuyItem(IProduct product)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            var otherVendingMachine = obj as VendingMachine;

            return otherVendingMachine != null &&
                Equals(otherVendingMachine.Catalog, Catalog) &&
                Equals(otherVendingMachine.Store, Store) &&
                Equals(otherVendingMachine.CashRegister, CashRegister) &&
                Equals(otherVendingMachine.ControlPanel, ControlPanel);
        }

        public override int GetHashCode()
        {
            return Catalog.GetHashCode() ^ Store.GetHashCode() ^ CashRegister.GetHashCode() ^ ControlPanel.GetHashCode();
        }
    }
}
