using System.Linq;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Data
{
    public class ControlPanelState
    {
        public virtual int Id { get; set; }
        public virtual string InsertedCoinsTypesNames { get; set; }

        public ControlPanelState()
        {
        }

        public ControlPanelState(IControlPanel controlPanel)
        {
            InsertedCoinsTypesNames = string.Join(",", controlPanel.InsertedCoins.Select(coin => coin.Type));
        }
    }
}
