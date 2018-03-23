using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VendingMachine.UI.Controls.ViewModels;

namespace VendingMachine.UI.Controls
{
    public class StoreView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private StoreViewModel model;
        public StoreViewModel Model
        {
            get { return model; }
            set
            {
                if (value != model)
                {
                    model = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Model)));

                    if (model != null)
                    {
                        RegenerateSlotsViews();
                    }
                }
            }
        }

        private readonly TableLayoutPanel slotsViewsLayout = null;

        private void RegenerateSlotsViews()
        {
            slotsViewsLayout.Controls.Clear();
            slotsViewsLayout.ColumnCount = 0;
            slotsViewsLayout.ColumnStyles.Clear();

            foreach (var slot in Model.Store)
            {
                slotsViewsLayout.ColumnCount += 1;

                var slotViewModel = new StoreSlotViewModel(slot);
                var slotView = new VendingMachineStoreSlotView(slotViewModel)
                {
                    Dock = DockStyle.Fill
                };

                slotsViewsLayout.Controls.Add(slotView, slotsViewsLayout.ColumnCount - 1, 0);
            }

            var columnWidth = 100f / slotsViewsLayout.ColumnCount;
            for (int i = 1; i <= slotsViewsLayout.ColumnCount; i++)
            {
                var columnStyle = new ColumnStyle(SizeType.Percent, columnWidth);
                slotsViewsLayout.ColumnStyles.Add(columnStyle);
            }
        }

        public StoreView()
        {
            slotsViewsLayout  = new TableLayoutPanel
            {
                RowCount = 1,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGreen
            };

            this.Controls.Add(slotsViewsLayout);
        }

        public StoreView(StoreViewModel model) : this()
        {
            Model = model;
        }
    }
}
