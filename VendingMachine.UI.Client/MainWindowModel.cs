﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;
using VendingMachine.Data;

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

        private string currentVendingMachineName;
        public string CurrentVendingMachineName
        {
            get { return currentVendingMachineName; }
            set
            {
                if (value != currentVendingMachineName)
                {
                    currentVendingMachineName = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentVendingMachineName)));
                }
            }
        }

        private int currentVendingMachineId;

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
            var newVendingMachine = new LombardOdierVendingMachine(Configuration.StoreSlotsCapacity, Configuration.CashRegisterSlotsCapacity);

            foreach (var catalogEntry in newVendingMachine.Catalog)
            {
                var initialItems = ItemsFactory.Make(catalogEntry.Product, Configuration.InitialBottlesCount);

                newVendingMachine.Feed(initialItems);
            }

            CurrentVendingMachine = newVendingMachine;
            CurrentVendingMachineName = null;
            currentVendingMachineId = 0;
        }

        public IEnumerable<VendingMachineState> GetVendingMachineStates()
        {
            using (var vendingMachinesStatesRepository = new VendingMachinesStatesRepository())
            {
                return vendingMachinesStatesRepository.GetAllStates();
            }
        }

        public void OpenState(VendingMachineState state)
        {
            var vendingMachine = VendingMachinesStatesRepository.GetVendingMachineFromState(state);

            CurrentVendingMachine = vendingMachine;
            CurrentVendingMachineName = state.Name;
            currentVendingMachineId = state.Id;
        }

        public void SaveVendingMachineState(string name)
        {
            CurrentVendingMachineName = name;

            using (var vendingMachinesStatesRepository = new VendingMachinesStatesRepository())
            {
                currentVendingMachineId = vendingMachinesStatesRepository.SaveStateOf(CurrentVendingMachine, CurrentVendingMachineName, currentVendingMachineId);
            }
        }

        public void ResetDatabase()
        {
            VendingMachinesStatesRepository.ResetDatabase();
        }

        public void ExitApplication()
        {
            Application.Exit();
        }
    }
}
