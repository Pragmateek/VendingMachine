using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface IVendingMachineStore : IEnumerable<IVendingMachineStoreSlot>
    {
    }
}
