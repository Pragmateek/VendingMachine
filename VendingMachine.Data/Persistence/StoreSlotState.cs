using VendingMachine.Business.Contracts;

namespace VendingMachine.Data
{
    public class StoreSlotState
    {
        public virtual int Id { get; set; }
        public virtual string CatalogProductName { get; set; }
        public virtual uint Capacity { get; set; }
        public virtual uint Count { get; set; }
        public virtual StoreState StoreState { get; set; }

        public StoreSlotState()
        {
        }

        public StoreSlotState(IStoreSlot storeSlot)
        {
            CatalogProductName = storeSlot.CatalogEntry.Product.Name;
            Capacity = storeSlot.Capacity;
            Count = storeSlot.Count;
        }
    }
}
