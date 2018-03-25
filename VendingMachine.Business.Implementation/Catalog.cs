using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;
using VendingMachine.Tools;

namespace VendingMachine.Business.Implementation
{
    /// <summary>
    /// Minimal implementation of <see cref="ICatalog"/>.
    /// </summary>
    public class Catalog : ICatalog
    {
        public static Catalog Empty = new Catalog();

        private IList<ICatalogEntry> entries = new List<ICatalogEntry>();

        public void ReferenceProduct(IProduct product, IPrice price)
        {
            var newEntry = new CatalogEntry(product, price);

            entries.Add(newEntry);
        }

        public ICatalogEntry GetEntryFor(IProduct product)
        {
            return entries.FirstOrDefault(entry => entry.Product == product);
        }

        public override bool Equals(object obj)
        {
            var otherCatalog = obj as Catalog;

            return otherCatalog != null && otherCatalog.SequenceEqual(this);
        }

        public override int GetHashCode()
        {
            return entries.GetElementsHashCode();
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
