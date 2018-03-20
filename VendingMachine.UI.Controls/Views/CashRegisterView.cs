using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using VendingMachine.UI.Controls.ViewModels;

namespace VendingMachine.UI.Controls
{
    public class CashRegisterView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private CashRegisterViewModel model;
        public CashRegisterViewModel Model
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

        public CashRegisterView()
        {
        }

        public CashRegisterView(CashRegisterViewModel model)
        {
            var layout = new TableLayoutPanel
            {
                AutoScroll = true,
                RowCount = 1,
                ColumnCount = model.CashRegister.Slots.Count(),
                Dock = DockStyle.Fill
            };

            int column = 0;
            foreach (var cashRegisterSlot in model.CashRegister.Slots)
            {
                var cashRegisterSlotViewModel = new CashRegisterSlotViewModel(cashRegisterSlot);
                var cashRegisterSlotView = new CashRegisterSlotView(cashRegisterSlotViewModel);
                layout.Controls.Add(cashRegisterSlotView, column, 0);
                column += 1;
            }

            Controls.Add(layout);
        }
    }
}
