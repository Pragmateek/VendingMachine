using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VendingMachine.UI.Controls.ViewModels;

namespace VendingMachine.UI.Controls
{
    public class VendingMachineView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private VendingMachineViewModel model;
        public VendingMachineViewModel Model
        {
            get { return model; }
            set
            {
                if (value != model)
                {
                    model = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Model)));
                }
            }
        }

        public VendingMachineView(VendingMachineViewModel model)
        {
            Model = model;

            var partsLayout  = new TableLayoutPanel
            {
                RowCount = 2,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                //BackColor = Color.LightPink
            };
            partsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            partsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            partsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 35));
            partsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 65));

            var storeViewModel = new StoreViewModel(model.VendingMachine.Store);
            var storeView = new StoreView(storeViewModel)
            {
                Dock = DockStyle.Fill
            };

            var cashRegisterViewModel = new CashRegisterViewModel(model.VendingMachine.CashRegister);
            var cashRegisterView = new CashRegisterView(cashRegisterViewModel)
            {
                Dock = DockStyle.Fill
            };

            var controlPanelViewModel = new ControlPanelViewModel(model.VendingMachine.ControlPanel);
            var controlPanelView = new ControlPanelView(controlPanelViewModel)
            {
                Dock = DockStyle.Fill
            };

            partsLayout.Controls.Add(storeView, 0, 0);
            partsLayout.SetRowSpan(storeView, 2);

            partsLayout.Controls.Add(controlPanelView, 1, 0);

            partsLayout.Controls.Add(cashRegisterView, 1, 1);

            Controls.Add(partsLayout);
        }
    }
}
