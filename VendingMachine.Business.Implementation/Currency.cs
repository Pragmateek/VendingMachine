using System;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class Currency : ICurrency
    {
        public string Name { get; private set; }

        public Currency(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "A currency must have a name!");
        }

        public override bool Equals(object obj)
        {
            var otherCurrency = obj as Currency;

            return otherCurrency.Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
