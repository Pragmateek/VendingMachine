using System.ComponentModel;
using VendingMachine.Business;

namespace VendingMachine.UI.Controls.ViewModels
{
    public class CashRegisterSlotViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private ICashRegisterSlot cashRegisterSlot;
        public ICashRegisterSlot CashRegisterSlot
        {
            get { return cashRegisterSlot; }
            set
            {
                if (value != cashRegisterSlot)
                {
                    cashRegisterSlot = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(CashRegisterSlot)));
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(CashRegisterSlotCoinImagePath)));
                }
            }
        }

        public string CashRegisterSlotCoinImagePath => $"./Images/Coins/{CashRegisterSlot.CoinType.Name.Replace(" ", "")}.png";

        public string CountCapacityText => $"{CashRegisterSlot.Count} / {CashRegisterSlot.Capacity}";

        public CashRegisterSlotViewModel(ICashRegisterSlot cashRegisterSlot)
        {
            this.cashRegisterSlot = cashRegisterSlot;
        }
    }
}
