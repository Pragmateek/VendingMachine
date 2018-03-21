﻿using System.Collections.Generic;

namespace VendingMachine.Business.Contracts
{
    public interface ICashRegister
    {
        IEnumerable<ICashRegisterSlot> Slots { get; }
        decimal Amount { get; }

        bool TryPut(ICoin coin);

        bool TryGetChange(decimal amount, out IEnumerable<ICoin> cash);
    }
}
