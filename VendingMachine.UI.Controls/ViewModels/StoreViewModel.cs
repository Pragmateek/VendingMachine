using System.ComponentModel;
using VendingMachine.Business.Contracts;

namespace VendingMachine.UI.Controls.ViewModels
{
    public class StoreViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IStore store;
        public IStore Store
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

        public StoreViewModel(IStore store)
        {
            this.store = store;
        }
    }
}
