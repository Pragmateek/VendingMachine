using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface IStore
    {
        IEnumerable<IStoreSlot> Slots { get; }

        void Put(IEnumerable<IItem> newItems);

        bool TryGet(IProduct product, out IItem item);
    }
}
