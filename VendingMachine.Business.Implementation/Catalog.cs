using System.Collections;
using System.Collections.Generic;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class Catalog : ICatalog
    {
        private IList<ICatalogEntry> entries = new List<ICatalogEntry>();

        public void ReferenceProduct(IProduct product, IPrice price)
        {
            var newEntry = new CatalogEntry(product, price);

            entries.Add(newEntry);
        }

        public IEnumerator<ICatalogEntry> GetEnumerator()
        {
            return entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return entries.GetEnumerator();
        }
    }
}
