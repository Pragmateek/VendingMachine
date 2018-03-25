using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class ItemsFactory
    {
        public class VendingMachineItemsFactoryProduction : IEnumerable<IItem>
        {
            private IList<IItem> items = new List<IItem>();

            public VendingMachineItemsFactoryProduction(IProduct initialProduct, uint quantity)
            {
                Then(initialProduct, quantity);
            }

            public VendingMachineItemsFactoryProduction Then(IProduct product, uint quantity)
            {
                for (int i = 1; i <= quantity; i++)
                {
                    var newItem = new Item(product);
                    items.Add(newItem);
                }

                return this;
            }

            public IEnumerator<IItem> GetEnumerator()
            {
                return items.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public static VendingMachineItemsFactoryProduction Make(IProduct product, uint quantity)
        {
            return new VendingMachineItemsFactoryProduction(product, quantity);
        }

        public static IItem Make(IProduct product)
        {
            return Make(product, 1).Single();
        }
    }
}
