namespace VendingMachine.Business.Contracts
{
    public interface ICashRegisterSlot
    {
        ICoinType CoinType { get; }

        uint Capacity { get; }
        uint Count { get; }

        bool IsFull { get; }

        decimal Amount { get; }

        bool TryPut(ICoin coin);
    }
}
