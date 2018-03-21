using System.ComponentModel;
using VendingMachine.Business.Contracts;

namespace VendingMachine.UI.Controls.ViewModels
{
    public class VendingMachineStoreSlotViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IVendingMachineStoreSlot slot;
        public IVendingMachineStoreSlot Slot
        {
            get { return slot; }
            set
            {
                if (value != slot)
                {
                    slot = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Slot)));
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(SlotProductImagePath)));
                }
            }
        }

        public string SlotProductImagePath => $"./Images/Products/{Slot.CatalogEntry.Product.Name}.png";

        public string ProductPriceText => Slot.CatalogEntry.Price.ToString();

        public string CountCapacityText => $"{Slot.Count} / {Slot.Capacity}";

        public VendingMachineStoreSlotViewModel(IVendingMachineStoreSlot slot)
        {
            this.slot = slot;
        }
    }
}
