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
            var numberOfCoinsTypes = model.CashRegister.Slots.Count();

            var layout = new TableLayoutPanel
            {
                AutoScroll = true,
                ColumnCount = 1,
                RowCount = numberOfCoinsTypes,
                Dock = DockStyle.Fill
            };

            var rowHeight = 100f / numberOfCoinsTypes;

            int row = 0;
            foreach (var cashRegisterSlot in model.CashRegister.Slots)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, rowHeight));

                var cashRegisterSlotViewModel = new CashRegisterSlotViewModel(cashRegisterSlot);
                var cashRegisterSlotView = new CashRegisterSlotView(cashRegisterSlotViewModel)
                {
                    Dock = DockStyle.Fill
                };
                layout.Controls.Add(cashRegisterSlotView, 0, row);
                row += 1;
            }

            Controls.Add(layout);
        }
    }
}
