using System;
using System.ComponentModel;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class CashRegisterSlot : ICashRegisterSlot, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public ICoinType CoinType { get; }

        public uint Capacity { get; }
        private uint count;
        public uint Count
        {
            get { return count; }
            set
            {
                if (value != count)
                {
                    count = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Count)));
                }
            }
        }

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

        public void Remove(ICoin coin)
        {
            Count--;
        }
    }
}
