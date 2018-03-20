using System.ComponentModel;
using VendingMachine.Business;

namespace VendingMachine.UI.Controls.ViewModels
{
    public class CashRegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private ICashRegister cashRegister;
        public ICashRegister CashRegister
        {
            get { return cashRegister; }
            set
            {
                if (value != cashRegister)
                {
                    cashRegister = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(CashRegister)));
                }
            }
        }

        public CashRegisterViewModel(ICashRegister cashRegister)
        {
            this.cashRegister = cashRegister;
        }
    }
}
