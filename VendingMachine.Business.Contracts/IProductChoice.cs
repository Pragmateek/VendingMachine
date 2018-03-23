namespace VendingMachine.Business.Contracts
{
    public interface IProductChoice
    {
        ICatalogEntry CatalogEntry { get; }
        bool IsPossible { get; }
    }
}
