namespace VendingMachine.Business
{
    public interface IVendingMachine
    {
        IVendingMachineInventory AvailableItems { get; }

        ICurrency Currency { get; }
        decimal InsertedAmount { get; }

        IVendingMachine Insert(ICoin coin);

        bool TryBuyItem(IVendingMachineItem item);
    }
}
