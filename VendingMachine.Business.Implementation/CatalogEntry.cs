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

        public override bool Equals(object obj)
        {
            var otherCatalogEntry = obj as CatalogEntry;

            return otherCatalogEntry != null && otherCatalogEntry.Price.Equals(Price) && otherCatalogEntry.Product.Equals(Product);
        }

        public override int GetHashCode()
        {
            return Product.GetHashCode() ^ Price.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Product}: {Price}";
        }
    }
}
