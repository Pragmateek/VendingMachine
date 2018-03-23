using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface IStore : IEnumerable<IStoreSlot>
    {
        void Put(IEnumerable<IItem> newItems);
    }
}
