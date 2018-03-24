using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Business.Implementation;
using Implementation = VendingMachine.Business.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Data;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Tests
{
    [TestClass]
    public class VendingMachineStatePersistenceTests
    {
        [TestMethod]
        public void CanSaveAndRetrieveAMachineState()
        {
            var catalog = new Catalog();
            catalog.ReferenceProduct(new Product("Evian"), new Price(CurrenciesRepository.CHF, 1.1m));
            catalog.ReferenceProduct(new Product("Vittel"), new Price(CurrenciesRepository.CHF, 1.2m));
            catalog.ReferenceProduct(new Product("Volvic"), new Price(CurrenciesRepository.CHF, 1.3m));

            var inputVendingMachine = new Implementation.VendingMachine(catalog, 20, CoinsTypesRepository.SwissFrancCoins, 100);

            int stateId;
            using (var repository = new VendingMachinesStatesRepository())
            {
                stateId = repository.SaveStateOf(inputVendingMachine);
            }

            IVendingMachine outputVendingMachine;
            using (var repository = new VendingMachinesStatesRepository())
            {
                var vendingMachineState = repository.GetStateById(stateId);

                outputVendingMachine = repository.GetVendingMachineFromState(vendingMachineState);
            }
        }
    }
}
