using System;
using System.Collections.Generic;
using System.ComponentModel;
using VendingMachine.Data;
using VendingMachine.Tools;

namespace VendingMachine.UI.Controls.ViewModels
{
    public class VendingMachinesStatesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public event EventHandler<ValueEventArgs<VendingMachineState>> StateOpeningRequested = delegate { };

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

        private VendingMachineState selectedState;
        public VendingMachineState SelectedState
        {
            get { return selectedState; }
            set
            {
                if (value != selectedState)
                {
                    selectedState = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedState)));
                }
            }
        }

        public VendingMachinesStatesViewModel(IEnumerable<VendingMachineState> vendingMachinesStates)
        {
            this.vendingMachinesStates = vendingMachinesStates;
        }

        public void OpenSelectedState()
        {
            StateOpeningRequested(this, new ValueEventArgs<VendingMachineState>(SelectedState));
        }
    }
}
