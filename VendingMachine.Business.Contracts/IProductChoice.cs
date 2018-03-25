namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents a choice a customer can make to buy a product.
    /// </summary>
    public interface IProductChoice
    {
        /// <summary>
        /// Get the catalog-entry exposed by this choice.
        /// </summary>
        ICatalogEntry CatalogEntry { get; }

        /// <summary>
        /// Check if this choice can be made.
        /// This depends on items availability and inserted money.
        /// </summary>
        bool IsPossible { get; }
    }
}
