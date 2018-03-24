using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class Price : IPrice
    {
        public ICurrency Currency { get; private set; }
        public decimal Amount { get; private set; }

        public Price(ICurrency currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public override bool Equals(object obj)
        {
            var otherPrice = obj as Price;

            return otherPrice != null && otherPrice.Currency == Currency && otherPrice.Amount == Amount;
        }

        public override int GetHashCode()
        {
            return Currency.GetHashCode() ^ Amount.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Currency} {Amount}";
        }
    }
}
