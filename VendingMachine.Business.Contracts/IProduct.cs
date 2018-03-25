namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Represents a product in general, not a specific instance of it.
    /// e.g. "the Evian 50cl bottle" is a product, who has many instances in the form of concrete bottles.
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// The name of this product.
        /// </summary>
        string Name { get; }
    }
}
