using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachineControlPanel : IVendingMachineControlPanel
    {
        public decimal InsertedAmount => throw new NotImplementedException();

        public VendingMachineControlPanel(IVendingMachine vendingMachine)
        {

        }

        public void Insert(ICoin coin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICoin> Refund()
        {
            throw new NotImplementedException();
        }

        public bool TryBuy(IVendingMachineProduct product)
        {
            throw new NotImplementedException();
        }
    }
}
