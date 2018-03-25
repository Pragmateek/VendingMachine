using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents a vending-machine.
    /// Acts as a facade for the underlying parts: store, cash-register, control-panel...
    /// </summary>
    public interface IVendingMachine
    {
        /// <summary>
        /// The catalog of products generally available in this vending-machine.
        /// </summary>
        ICatalog Catalog { get; }

        /// <summary>
        /// The coin-types this vending-machine accepts.
        /// </summary>
        IEnumerable<ICoinType> AcceptedCoinsTypes { get; }

        /// <summary>
        /// The amount of money the user has available to buy items from this vending-machine.
        /// </summary>
        decimal InsertedAmount { get; }

        /// <summary>
        /// The store used by this vending-machine to store its items.
        /// </summary>
        IStore Store { get; }

        /// <summary>
        /// The cash-register used by this vending-machine to store its money.
        /// </summary>
        ICashRegister CashRegister { get; }

        /// <summary>
        /// The control-panel used by this vending-machine to interact with the customer.
        /// </summary>
        IControlPanel ControlPanel { get; }

        /// <summary>
        /// Feed some items into this vending-machine.
        /// </summary>
        /// <param name="items">The items to add.</param>
        void Feed(IEnumerable<IItem> items);

        /// <summary>
        /// Insert some soin into this vending-machine.
        /// </summary>
        /// <param name="coin">A coin.</param>
        /// <returns>True if the coin has been accepted.</returns>
        bool TryInsert(ICoin coin);

        /// <summary>
        /// Insert many coins into this vending-machine.
        /// </summary>
        /// <param name="coins">Many coins.</param>
        void Insert(IEnumerable<ICoin> coins);

        /// <summary>
        /// Try to buy some product from this vending-machine.
        /// </summary>
        /// <param name="product">The product to buy.</param>
        /// <param name="item">The bought item, it buy was possible.</param>
        /// <returns>True if the product could be bought.</returns>
        bool TryBuyItem(IProduct product, out IItem item);
    }
}
