using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class Item : IItem
    {
        public IProduct Product { get; private set; }

        public Item(IProduct product)
        {
            Product = product;
        }
    }
}
