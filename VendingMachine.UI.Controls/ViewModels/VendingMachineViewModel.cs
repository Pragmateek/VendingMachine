using System.ComponentModel;
using VendingMachine.Business.Contracts;

namespace VendingMachine.UI.Controls.ViewModels
{
    public class VendingMachineViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IVendingMachine vendingMachine;
        public IVendingMachine VendingMachine
        {
            get { return vendingMachine; }
            set
            {
                if (value != vendingMachine)
                {
                    vendingMachine = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(VendingMachine)));
                }
            }
        }

        public VendingMachineViewModel(IVendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
        }
    }
}
