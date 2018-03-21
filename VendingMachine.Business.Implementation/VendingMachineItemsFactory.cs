using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class VendingMachineItemsFactory
    {
        public class VendingMachineItemsFactoryProduction : IEnumerable<IVendingMachineItem>
        {
            private IList<IVendingMachineItem> items = new List<IVendingMachineItem>();

            public VendingMachineItemsFactoryProduction(IVendingMachineProduct initialProduct, uint quantity)
            {
                Then(initialProduct, quantity);
            }

            public VendingMachineItemsFactoryProduction Then(IVendingMachineProduct product, uint quantity)
            {
                for (int i = 1; i <= quantity; i++)
                {
                    var newItem = new VendingMachineItem(product);
                    items.Add(newItem);
                }

                return this;
            }

            public IEnumerator<IVendingMachineItem> GetEnumerator()
            {
                return items.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public static VendingMachineItemsFactoryProduction Make(IVendingMachineProduct product, uint quantity)
        {
            return new VendingMachineItemsFactoryProduction(product, quantity);
        }
    }
}
