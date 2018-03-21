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

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
