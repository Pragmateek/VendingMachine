using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents a cash-register to store a vending-machine coins.
    /// </summary>
    public interface ICashRegister
    {
        /// <summary>
        /// Coins are spread over slots, one per coin type.
        /// </summary>
        IEnumerable<ICashRegisterSlot> Slots { get; }

        /// <summary>
        /// The total amount of money stored in this cash-register.
        /// </summary>
        decimal Amount { get; }

        /// <summary>
        /// Try to add a single coin to this cash-register.
        /// </summary>
        /// <param name="coin">A coin.</param>
        /// <returns>True if the coin was successfully added, false otherwise.</returns>
        bool TryPut(ICoin coin);

        /// <summary>
        /// Add many coins to this cash-register.
        /// </summary>
        /// <param name="coins">Many coins.</param>
        void Put(IEnumerable<ICoin> coins);

        /// <summary>
        /// Check if this cash-register can provide the change on a given amount of money.
        /// </summary>
        /// <param name="amount">An amount of money.</param>
        /// <returns>True if it can provide the change, false otherwise.</returns>
        bool CanGetChangeOn(decimal amount);

        /// <summary>
        /// Try to get the change on a given amount of money.
        /// </summary>
        /// <param name="amount">An amount of money.</param>
        /// <param name="coins">All the change's coins if change was possible, otherwise an empty enumerable.</param>
        /// <returns>True if change was possible, false otherwise.</returns>
        bool TryGetChange(decimal amount, out IEnumerable<ICoin> coins);

        /// <summary>
        /// Remove coins from this cash-register.
        /// </summary>
        /// <param name="coins">Coins to remove from the cash-register.</param>
        void Remove(IEnumerable<ICoin> coins);
    }
}
