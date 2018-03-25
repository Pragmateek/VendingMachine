using System;
using System.ComponentModel;
using VendingMachine.Business.Contracts;

namespace VendingMachine.UI.Controls.ViewModels
{
    public class StoreSlotViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IStoreSlot slot;
        public IStoreSlot Slot
        {
            get { return slot; }
            set
            {
                if (value != slot)
                {
                    if (slot != null)
                    {
                        slot.ItemsChanged -= Slot_ItemsChanged;
                    }
                    slot = value;
                    if (slot != null)
                    {
                        slot.ItemsChanged += Slot_ItemsChanged;
                    }
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Slot)));
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(SlotProductImagePath)));
                }
            }
        }

        public string SlotProductImagePath => $"./Images/Products/{Slot.CatalogEntry.Product.Name.Replace(" ", "")}.png";

        public string ProductNameText => Slot.CatalogEntry.Product.Name.ToUpperInvariant();

        public string ProductPriceText => Slot.CatalogEntry.Price.ToString();

        public string CountCapacityText => $"{Slot.Count} / {Slot.Capacity}";

        public StoreSlotViewModel(IStoreSlot slot)
        {
            Slot = slot;
        }

        private void Slot_ItemsChanged(object sender, EventArgs e)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(CountCapacityText)));
        }
    }
}
