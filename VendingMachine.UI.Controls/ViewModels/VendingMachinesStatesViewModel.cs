using System.Collections.Generic;
using System.ComponentModel;
using VendingMachine.Business.Contracts;
using VendingMachine.Data;

namespace VendingMachine.UI.Controls.ViewModels
{
    public class VendingMachinesStatesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IEnumerable<VendingMachineState> vendingMachinesStates;
        public IEnumerable<VendingMachineState> VendingMachinesStates
        {
            get { return vendingMachinesStates; }
            set
            {
                if (value != vendingMachinesStates)
                {
                    vendingMachinesStates = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(VendingMachinesStates)));
                }
            }
        }

        public VendingMachinesStatesViewModel(IEnumerable<VendingMachineState> vendingMachinesStates)
        {
            this.vendingMachinesStates = vendingMachinesStates;
        }
    }
}
