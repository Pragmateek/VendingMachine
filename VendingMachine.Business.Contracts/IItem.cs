namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents an item that can be bought from a vending-machine.
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// The products from which this item is an instance.
        /// </summary>
        IProduct Product { get; }
    }
}
