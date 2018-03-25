namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents a cash-register slot used to store coins of a given type.
    /// </summary>
    public interface ICashRegisterSlot
    {
        /// <summary>
        /// The coin type this slot can store.
        /// </summary>
        ICoinType CoinType { get; }

        /// <summary>
        /// The maximum number of coins this slot can store.
        /// </summary>
        uint Capacity { get; }

        /// <summary>
        /// The current number of coins in this slot.
        /// </summary>
        uint Count { get; }

        /// <summary>
        /// True if this slot is full, i.e. it can't store more coins.
        /// </summary>
        bool IsFull { get; }

        /// <summary>
        /// The total amount of money stored in this slot.
        /// </summary>
        decimal Amount { get; }

        /// <summary>
        /// Try to add a coin to this slot.
        /// </summary>
        /// <param name="coin">A coin.</param>
        /// <returns>True if there was enough room to store the coin, false otherwise.</returns>
        bool TryPut(ICoin coin);

        /// <summary>
        /// Remove a coin from this slot.
        /// </summary>
        /// <param name="coin">The coin to remove.</param>
        void Remove(ICoin coin);
    }
}
