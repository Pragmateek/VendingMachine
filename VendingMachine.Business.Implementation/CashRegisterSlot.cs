using System;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class CashRegisterSlot : ICashRegisterSlot
    {
        public ICoinType CoinType { get; }

        public uint Capacity { get; }
        public uint Count { get; private set; }

        public bool IsFull => Count == Capacity;

        public decimal Amount => Count * CoinType.FaceValue;

        public CashRegisterSlot(ICoinType coinType, uint capacity)
        {
            CoinType = coinType;
            Capacity = capacity;
        }

        public bool TryPut(ICoin coin)
        {
            if (coin.Type != CoinType)
            {
                throw new Exception($"This slot does not handle '{coin.Type}'!");
            }

            if (IsFull)
            {
                return false;
            }

            Count++;

            return true;
        }
    }
}
