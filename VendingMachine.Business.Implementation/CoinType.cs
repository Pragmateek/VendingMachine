using System;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class CoinType : ICoinType
    {
        public string Name { get; }
        public ICurrency Currency { get; }
        public decimal FaceValue { get; }

        public CoinType(string name, ICurrency currency, decimal faceValue)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "A coin type must have a name!");
            Currency = currency ?? throw new ArgumentNullException(nameof(currency), "A coin type must have a currency!"); ;
            FaceValue = faceValue;
        }

        public override bool Equals(object obj)
        {
            var otherCoinType = obj as CoinType;

            return otherCoinType != null &&
                otherCoinType.Name == Name &&
                Equals(otherCoinType.Currency, Currency) &&
                otherCoinType.FaceValue == FaceValue;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Currency.GetHashCode() ^ FaceValue.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
