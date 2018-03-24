using VendingMachine.Business.Contracts;

namespace VendingMachine.Data
{
    public class CashRegisterSlotState
    {
        public virtual int Id { get; set; }
        public virtual string CoinTypeName { get; }
        public virtual uint Capacity { get; set; }
        public virtual uint Count { get; set; }

        public CashRegisterSlotState()
        {
        }

        public CashRegisterSlotState(ICashRegisterSlot cashRegisterSlot)
        {
            CoinTypeName = cashRegisterSlot.CoinType.Name;
            Capacity = cashRegisterSlot.Capacity;
            Count = cashRegisterSlot.Count;
        }
    }
}
