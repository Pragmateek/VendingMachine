namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents a type of money coin.
    /// As an example an instance could represent the "1 Swiss Franc" coin in general, not a specific "1 Swiss Franc" coin.
    /// </summary>
    public interface ICoinType
    {
        /// <summary>
        /// The name of this type of money coin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The currency of this type of money coin.
        /// </summary>
        ICurrency Currency { get; }

        /// <summary>
        /// The face value of this type of money coin.
        /// </summary>
        decimal FaceValue { get; }
    }
}
