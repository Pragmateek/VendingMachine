using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    /// <summary>
    /// Acts as a facade for the underlying parts: store, cash-register, control-panel...
    /// </summary>
    public interface IVendingMachine
    {
        uint Capacity { get; }

        IVendingMachineStore Store { get; }
        ICashRegister CashRegister { get; }
        IVendingMachineControlPanel ControlPanel { get; }

        ICurrency Currency { get; }
        
        bool TryBuyItem(IVendingMachineProduct product);
    }
}
