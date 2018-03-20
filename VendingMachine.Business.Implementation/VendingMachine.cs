using System;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachine : IVendingMachine
    {
        protected VendingMachineInventory availableItems = new VendingMachineInventory();

        public IVendingMachineInventory AvailableItems => availableItems;

        public ICurrency Currency { get; private set; }
        public decimal InsertedAmount { get; private set; }

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
