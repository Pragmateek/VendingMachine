using System.ComponentModel;

namespace VendingMachine.UI.Client
{
    public class Configuration : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private uint storeSlotsCapacity;
        public uint StoreSlotsCapacity
        {
            get { return storeSlotsCapacity; }
            set
            {
                if (value != storeSlotsCapacity)
                {
                    storeSlotsCapacity = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(StoreSlotsCapacity)));
                }
            }
        }

        private uint cashRegisterSlotsCapacity;
        public uint CashRegisterSlotsCapacity
        {
            get { return cashRegisterSlotsCapacity; }
            set
            {
                if (value != cashRegisterSlotsCapacity)
                {
                    cashRegisterSlotsCapacity = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(CashRegisterSlotsCapacity)));
                }
            }
        }

        private uint initialBottlesCount;
        public uint InitialBottlesCount
        {
            get { return initialBottlesCount; }
            set
            {
                if (value != initialBottlesCount)
                {
                    initialBottlesCount = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(InitialBottlesCount)));
                }
            }
        }

        private string databasePath;
        public string DatabasePath
        {
            get { return databasePath; }
            set
            {
                if (value != databasePath)
                {
                    databasePath = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(DatabasePath)));
                }
            }
        }
    }
}
