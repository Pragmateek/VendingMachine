using System.Collections;
using System.Collections.Generic;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachineStore : IVendingMachineStore
    {
        private readonly IList<IVendingMachineStoreSlot> slots = new List<IVendingMachineStoreSlot>();

        public void Add(IEnumerable<IVendingMachineItem> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public void Add(IVendingMachineItem item)
        {
            //items.Add(item);
        }

        public IEnumerator<IVendingMachineStoreSlot> GetEnumerator()
        {
            return slots.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return slots.GetEnumerator();
        }
    }
}
