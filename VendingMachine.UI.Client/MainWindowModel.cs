using System.ComponentModel;
using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;

namespace VendingMachine.UI.Client
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IVendingMachine currentVendingMachine;
        public IVendingMachine CurrentVendingMachine
        {
            get { return currentVendingMachine; }
            set
            {
                if (value != currentVendingMachine)
                {
                    currentVendingMachine = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentVendingMachine)));
                }
            }
        }


        private Configuration configuration;
        public Configuration Configuration
        {
            get { return configuration; }
            set
            {
                if (value != configuration)
                {
                    configuration = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Configuration)));
                }
            }
        }

        public void NewVendingMachine()
        {
            CurrentVendingMachine = new LombardOdierVendingMachine(Configuration.StoreSlotsCapacity, Configuration.CashRegisterSlotsCapacity);
        }
    }
}
