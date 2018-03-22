namespace VendingMachine.Business.Contracts
{
    public interface IVendingMachineCatalogEntry
    {
        IVendingMachineProduct Product { get; }
        IPrice Price { get; }
    }
}
