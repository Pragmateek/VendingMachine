using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class Coin : ICoin
    {
        public ICoinType Type { get; }

        public Coin(ICoinType type)
        {
            Type = type;
        }

        public override bool Equals(object obj)
        {
            var otherCoin = obj as Coin;

            return otherCoin != null && Equals(otherCoin.Type, Type);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
