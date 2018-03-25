using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using VendingMachine.Data;
using VendingMachine.Tools;
using VendingMachine.UI.Controls;
using VendingMachine.UI.Controls.ViewModels;
using VendingMachine.UI.Controls.Views;

namespace VendingMachine.UI.Client
{
    public class MainWindow : Form
    {
        public MainWindowModel Model { get; set; }

        public MainWindow()
        {
        }

        public MainWindow(MainWindowModel model)
        {
            Model = model;
            DataBindings.Add(nameof(Text), Model, nameof(Model.CurrentVendingMachineName));

            Model.PropertyChanged += Model_PropertyChanged;

            Menu = new MainMenu();

            var fileItem = new MenuItem("File");
            fileItem.MenuItems.Add("New Vending Machine", NewVendingMachineMenuItem_Click);
            fileItem.MenuItems.Add("Open Vending Machine", OpenVendingMachineMenuItem_Click);
            fileItem.MenuItems.Add("Save Vending Machine", SaveVendingMachineStateMenuItem_Click);

            var toolsItem = new MenuItem("Tools");
            toolsItem.MenuItems.Add("Configuration", ConfigurationMenuItem_Click);

            Menu.MenuItems.Add(fileItem);
            Menu.MenuItems.Add(toolsItem);
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowModel.CurrentVendingMachine))
            {
                var vendingMachineViewModel = new VendingMachineViewModel(Model.CurrentVendingMachine);
                var vendingMachineView = new VendingMachineView(vendingMachineViewModel)
                {
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.FixedSingle
                };

                Controls.Clear();
                Controls.Add(vendingMachineView);
            }
        }

        private void NewVendingMachineMenuItem_Click(object sender, EventArgs args)
        {
            Model.NewVendingMachine();
        }

        private void OpenVendingMachineMenuItem_Click(object sender, EventArgs args)
        {
            var states = Model.GetVendingMachineStates();

            var vendingMachinesStatesViewModel = new VendingMachinesStatesViewModel(states);

            var statesView = new VendingMachinesStatesView(vendingMachinesStatesViewModel)
            {
                Dock = DockStyle.Fill
            };

            var popup = new Form
            {
                AutoSize = true
            };

            popup.Controls.Add(statesView);

            vendingMachinesStatesViewModel.StateOpeningRequested += (s, e) =>
            {
                popup.Close();
                Model.OpenState(e.Value);
            };

            popup.ShowDialog();
        }

        private void SaveVendingMachineStateMenuItem_Click(object sender, EventArgs args)
        {
            string name = Model.CurrentVendingMachineName;

            if (name == null)
            {
                name = Interaction.InputBox("Please provide a name for this vending machine", "Vending machine name");

                // If cancelled.
                if (name == "")
                {
                    return;
                }
            }

            Model.SaveVendingMachineState(name);
        }

        private void ConfigurationMenuItem_Click(object sender, EventArgs args)
        {
            var configurationViewModel = new ConfigurationViewModel(Model.Configuration);

            var configurationView = new ConfigurationView(configurationViewModel)
            {
                Dock = DockStyle.Fill
            };

            var popup = new Form
            {
                AutoSize = true,
                //AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            popup.Controls.Add(configurationView);

            configurationViewModel.ConfigurationSaved += (_1, _2) => popup.Close();

            popup.ShowDialog();
        }
    }
}
