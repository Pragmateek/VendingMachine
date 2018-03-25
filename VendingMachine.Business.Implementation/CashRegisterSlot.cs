using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class CashRegisterSlot : ICashRegisterSlot, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public ICoinType CoinType { get; }

        private IList<ICoin> coins = new List<ICoin>();

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
                // throw new Exception($"This slot does not handle '{coin.Type}'!");
                return false;
            }

            if (IsFull)
            {
                return false;
            }

            if (coins.Any(c => c == coin))
            {
                return false;
            }

            coins.Add(coin);
            Count++;

            return true;
        }

        public void Remove(ICoin coin)
        {
            if (coins.Remove(coin))
            {
                Count--;
            }
        }

        public override bool Equals(object obj)
        {
            var otherCashRegisterSlot = obj as CashRegisterSlot;

            return otherCashRegisterSlot != null &&
                otherCashRegisterSlot.CoinType == CoinType &&
                otherCashRegisterSlot.Capacity == Capacity &&
                otherCashRegisterSlot.Count == Count;
        }

        public override int GetHashCode()
        {
            return CoinType.GetHashCode() ^ Capacity.GetHashCode() ^ Count.GetHashCode();
        }
    }
}
