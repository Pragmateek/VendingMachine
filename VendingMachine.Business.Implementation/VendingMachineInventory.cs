using System.Collections;
using System.Collections.Generic;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachineInventory : IVendingMachineInventory
    {
        private readonly IList<IVendingMachineInventoryItem> items = new List<IVendingMachineInventoryItem>();

        public void Add(IVendingMachineInventoryItem item)
        {
            items.Add(item);
        }

        public IEnumerator<IVendingMachineInventoryItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
