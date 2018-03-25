using System;
using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents a store slot used to store instances of a given product.
    /// </summary>
    public interface IStoreSlot : IEnumerable<IItem>
    {
        /// <summary>
        /// Get the catalog-entry of the product stored in this slot.
        /// </summary>
        ICatalogEntry CatalogEntry { get; }

        /// <summary>
        /// Get the maximum number of items that can be stored in this slot.
        /// </summary>
        uint Capacity { get; }

        /// <summary>
        /// Get the current number of items stored in this slot.
        /// </summary>
        uint Count { get; }

        /// <summary>
        /// Raised when some items were added or removed from this slot.
        /// </summary>
        event EventHandler ItemsChanged;

        /// <summary>
        /// Store a new item in this slot.
        /// </summary>
        /// <param name="newItem"></param>
        void Store(IItem newItem);

        /// <summary>
        /// Store some new items in this slot.
        /// </summary>
        /// <param name="newItems"></param>
        void Store(IEnumerable<IItem> newItems);

        /// <summary>
        /// Try to get the next item from this slot.
        /// </summary>
        /// <returns></returns>
        IItem TakeOne();
    }
}
