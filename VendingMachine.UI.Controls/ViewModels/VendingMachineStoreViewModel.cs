using System.ComponentModel;
using VendingMachine.Business.Contracts;

namespace VendingMachine.UI.Controls.ViewModels
{
    public class VendingMachineStoreViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IVendingMachineStore store;
        public IVendingMachineStore Store
        {
            get { return store; }
            set
            {
                if (value != store)
                {
                    store = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Store)));
                }
            }
        }

        public VendingMachineStoreViewModel(IVendingMachineStore store)
        {
            this.store = store;
        }
    }
}
