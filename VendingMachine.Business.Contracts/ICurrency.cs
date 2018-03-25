namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents a currency.
    /// </summary>
    public interface ICurrency
    {
        /// <summary>
        /// The name of this currency.
        /// </summary>
        string Name { get; }
    }
}
