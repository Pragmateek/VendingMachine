using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Data
{
    public class StoreState
    {
        public virtual int Id { get; set; }
        public virtual StoreSlotState[] SlotsStates { get; set; }

        public StoreState()
        {
        }

        public StoreState(IStore store)
        {
            SlotsStates = store.Select(slot => new StoreSlotState(slot)).ToArray();
        }
    }
}
