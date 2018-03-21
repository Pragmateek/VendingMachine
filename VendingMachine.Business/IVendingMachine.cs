namespace VendingMachine.Business.Contracts
{
    public interface IVendingMachine
    {
        uint Capacity { get; }

        IVendingMachineStore AvailableItems { get; }

        ICurrency Currency { get; }
        decimal InsertedAmount { get; }

        IVendingMachine Insert(ICoin coin);

        bool TryBuyItem(IVendingMachineItem item);
    }
}
