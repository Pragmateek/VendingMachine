namespace VendingMachine.Business.Contracts
{
    public interface IPrice
    {
        ICurrency Currency { get; }

        decimal Amount { get; }
    }
}
