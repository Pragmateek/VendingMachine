namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents an entry in a catalog of products.
    /// </summary>
    public interface ICatalogEntry
    {
        /// <summary>
        /// The referenced product.
        /// </summary>
        IProduct Product { get; }

        /// <summary>
        /// The product's price.
        /// </summary>
        IPrice Price { get; }
    }
}
