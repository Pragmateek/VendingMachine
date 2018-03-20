namespace VendingMachine.Business
{
    public interface IPrice
    {
        ICurrency Currency { get; }

        decimal Amount { get; }
    }
}
