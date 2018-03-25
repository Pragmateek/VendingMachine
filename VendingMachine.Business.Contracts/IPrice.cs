namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents a price.
    /// </summary>
    public interface IPrice
    {
        /// <summary>
        /// The currency of this price.
        /// </summary>
        ICurrency Currency { get; }

        /// <summary>
        /// The amount of this price.
        /// </summary>
        decimal Amount { get; }
    }
}
