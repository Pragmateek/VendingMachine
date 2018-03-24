using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class CatalogEntry : ICatalogEntry
    {
        public IProduct Product { get; private set; }
        public IPrice Price { get; private set; }

        public CatalogEntry(IProduct product, IPrice price)
        {
            Product = product;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Product}: {Price}";
        }
    }
}
