using System.Linq;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Data
{
    public class ControlPanelState
    {
        public virtual int Id { get; set; }
        public string InsertedCoinsTypesNames { get; }

        public ControlPanelState()
        {
        }

        public ControlPanelState(IControlPanel controlPanel)
        {
            InsertedCoinsTypesNames = string.Join(",", controlPanel.InsertedCoins.Select(coin => coin.Type));
        }
    }
}
