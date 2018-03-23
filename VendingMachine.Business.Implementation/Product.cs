using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class Product : IProduct
    {
        public string Name { get; private set; }

        public Product(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
