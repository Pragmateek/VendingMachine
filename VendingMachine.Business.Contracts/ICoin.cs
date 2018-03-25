namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents a money coin.
    /// </summary>
    public interface ICoin
    {
        /// <summary>
        /// The coin's type.
        /// </summary>
        ICoinType Type { get; }
    }
}
