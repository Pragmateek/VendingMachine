using System.Linq;
using System.ComponentModel;
using VendingMachine.Business.Contracts;

namespace VendingMachine.UI.Controls.ViewModels
{
    public class ControlPanelViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IControlPanel controlPanel;
        public IControlPanel ControlPanel
        {
            get { return controlPanel; }
            set
            {
                if (value != controlPanel)
                {
                    controlPanel = value;
                    if (controlPanel is INotifyPropertyChanged)
                    {
                        ((INotifyPropertyChanged)controlPanel).PropertyChanged += ControlPanel_PropertyChanged;
                    }
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(ControlPanel)));
                }
            }
        }

        private void ControlPanel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IControlPanel.InsertedCoins))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(InsertedAmountText)));
            }
        }

        public string InsertedAmountText => $"{ControlPanel.AcceptedCoinsTypes.First().Currency} {ControlPanel.InsertedCoins.Sum(coin => coin.Type.FaceValue)}";

        public ControlPanelViewModel(IControlPanel controlPanel)
        {
            ControlPanel = controlPanel;
        }
    }
}
