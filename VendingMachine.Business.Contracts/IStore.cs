using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents a store for the items available in a vending-machine.
    /// </summary>
    public interface IStore
    {
        /// <summary>
        /// Items are spread over slots, one per product.
        /// </summary>
        IEnumerable<IStoreSlot> Slots { get; }

        /// <summary>
        /// Put some items in this store.
        /// </summary>
        /// <param name="newItems">The items to add.</param>
        void Put(IEnumerable<IItem> newItems);

        /// <summary>
        /// Check if some product is available in this store.
        /// </summary>
        /// <param name="product">A product.</param>
        /// <returns>True if this store has some items for this product.</returns>
        bool Has(IProduct product);

        /// <summary>
        /// Get the number of instances of a given product available in this store.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        uint NumberOf(IProduct product);

        /// <summary>
        /// Try to get some instance of a given product.
        /// </summary>
        /// <param name="product">A product.</param>
        /// <param name="item">The product instance if any.</param>
        /// <returns>True if a product instance has been found.</returns>
        bool TryGet(IProduct product, out IItem item);
    }
}
