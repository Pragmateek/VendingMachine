using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VendingMachine.UI.Controls.ViewModels;

namespace VendingMachine.UI.Controls
{
    public class CashRegisterSlotView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private CashRegisterSlotViewModel model;
        public CashRegisterSlotViewModel Model
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

        public CashRegisterSlotView()
        {
            var coinPicture = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill
            };
            coinPicture.DataBindings.Add("ImageLocation", this, "Model.CashRegisterSlotCoinImagePath");

            var countCapacityLabel = new Label
            {
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleCenter
            };
            countCapacityLabel.DataBindings.Add("Text", this, "Model.CountCapacityText");

            Controls.Add(coinPicture);
            Controls.Add(countCapacityLabel);
        }

        public CashRegisterSlotView(CashRegisterSlotViewModel model) : this()
        {
            Model = model;
        }
    }
}
