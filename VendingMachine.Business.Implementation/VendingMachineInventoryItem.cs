using System;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachineInventoryItem : IVendingMachineInventoryItem
    {
        public IVendingMachineItem Item { get; private set; }

        public IPrice Price { get; private set; }

        public uint Quantity { get; private set; }

        public VendingMachineInventoryItem(IVendingMachineItem item, IPrice price, uint quantity = 0)
        {
            Item = item;
            Price = price;
            Quantity = quantity;
        }

        public IVendingMachineInventoryItem AddItems(uint n)
        {
            Quantity += n;

            return this;
        }

        public IVendingMachineInventoryItem RemoveItems(uint n)
        {
            if (Quantity - n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), n, $"Cannot remove more items than available ({Quantity})!");
            }

            Quantity -= n;

            return this;
        }
    }
}
