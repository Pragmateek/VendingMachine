using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents the catalog of products available in a vending-machine.
    /// </summary>
    public interface ICatalog : IEnumerable<ICatalogEntry>
    {
        /// <summary>
        /// Reference a new product in the catalog.
        /// </summary>
        /// <param name="product">The product to reference.</param>
        /// <param name="price">The product's price.</param>
        void ReferenceProduct(IProduct product, IPrice price);

        /// <summary>
        /// Get the entry of a product in this catalog.
        /// </summary>
        /// <param name="product">The product to look for.</param>
        /// <returns>The entry for this product if any.</returns>
        ICatalogEntry GetEntryFor(IProduct product);
    }
}
