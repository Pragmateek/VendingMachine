using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface ICatalog : IEnumerable<ICatalogEntry>
    {
        void ReferenceProduct(IProduct product, IPrice price);
        ICatalogEntry GetEntryFor(IProduct product);
    }
}
