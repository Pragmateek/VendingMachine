using System.Linq;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Data
{
    public class CashRegisterState
    {
        public virtual int Id { get; set; }
        public virtual CashRegisterSlotState[] SlotsStates { get; set; }

        public CashRegisterState()
        {
        }

        public CashRegisterState(ICashRegister cashRegister)
        {
            SlotsStates = cashRegister.Slots.Select(slot => new CashRegisterSlotState(slot)).ToArray();
        }
    }
}
