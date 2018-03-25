using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents the control-panel of a vending-machine, i.e. the part that receive inputs from the customer (coins, choice, refund request...) and gives him feedback (inserted amount of money, available choices...).
    /// </summary>
    public interface IControlPanel
    {
        /// <summary>
        /// The types of money coins this control-panel can accept.
        /// Typically it may not accept coins whose face-value is too small.
        /// </summary>
        IEnumerable<ICoinType> AcceptedCoinsTypes { get; }

        /// <summary>
        /// The amount of money the customer has inserted, i.e. the amount available to buy items.
        /// </summary>
        decimal InsertedAmount { get; }

        /// <summary>
        /// Check if the customer can get some refund.
        /// This is not always possible: e.g. from a vending-machine without coins if you insert 2$ then buy a 1.50$ item, you can't get the remaining 0.50$ back.
        /// You'd have to insert 1.50$ so that the vending-machine can give you back the initial 2$ coin.
        /// </summary>
        bool CanRefund { get; }

        /// <summary>
        /// All the choices the user can make.
        /// Depending on the available items and the inserted amount of money some may not ne possible.
        /// </summary>
        IEnumerable<IProductChoice> ProductsChoices { get; }

        /// <summary>
        /// Try to insert a coin.
        /// </summary>
        /// <param name="coin">A coin.</param>
        /// <returns>True if the coin has been accepted.</returns>
        bool TryInsert(ICoin coin);

        /// <summary>
        /// Try to insert many coins.
        /// </summary>
        /// <param name="coins">Many coins.</param>
        void Insert(IEnumerable<ICoin> coins);

        /// <summary>
        /// Try to get some refund.
        /// </summary>
        /// <returns>The refund amount as coins, or an empty enumerable if refund was not possible.</returns>
        IEnumerable<ICoin> Refund();

        /// <summary>
        /// Try to buy a product.
        /// Only products available in store and whose price is covered by the inserted amount can be bought.
        /// </summary>
        /// <param name="product">The product to buy.</param>
        /// <param name="item">The bought item, or null if this was not possible.</param>
        /// <returns>True if the product has been successfully bought.</returns>
        bool TryBuy(IProduct product, out IItem item);
    }
}
