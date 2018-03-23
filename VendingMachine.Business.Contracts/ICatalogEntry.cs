namespace VendingMachine.Business.Contracts
{
    public interface ICatalogEntry
    {
        IProduct Product { get; }
        IPrice Price { get; }
    }
}
