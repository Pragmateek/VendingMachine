using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Data
{
    public class StoreState
    {
        public virtual int Id { get; set; }
        public virtual IList<StoreSlotState> SlotsStates { get; set; }
        public virtual VendingMachineState VendingMachineState { get; set; }

        public StoreState()
        {
        }

        public StoreState(IStore store)
        {
            SlotsStates = store.Slots.Select(slot => new StoreSlotState(slot)).ToList();
        }
    }
}
