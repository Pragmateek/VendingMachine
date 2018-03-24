using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Acts as a facade for the underlying parts: store, cash-register, control-panel...
    /// </summary>
    public interface IVendingMachine
    {
        IStore Store { get; }
        ICashRegister CashRegister { get; }
        IControlPanel ControlPanel { get; }
        
        bool TryBuyItem(IProduct product);
    }
}
