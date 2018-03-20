using VendingMachine.Business;

namespace VendingMachine.Implementation
{
    public class CoinType : ICoinType
    {
        public string Name { get; }
        public ICurrency Currency { get; }
        public decimal FaceValue { get; }

        public CoinType(string name, ICurrency currency, decimal faceValue)
        {
            Name = name;
            Currency = currency;
            FaceValue = faceValue;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
