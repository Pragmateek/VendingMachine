namespace VendingMachine.Business
{
    public interface IVendingMachineInventoryItem
    {
        IVendingMachineItem Item { get; }
        IPrice Price { get; }
        uint Quantity { get; }

        IVendingMachineInventoryItem AddItems(uint n);
        IVendingMachineInventoryItem RemoveItems(uint n);
    }
}
