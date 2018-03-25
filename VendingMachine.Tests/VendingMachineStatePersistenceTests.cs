using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Business.Implementation;
using Implementation = VendingMachine.Business.Implementation;
using VendingMachine.Data;
using VendingMachine.Business.Contracts;
using System;
using System.IO;

namespace VendingMachine.Tests
{
    [TestClass]
    public class VendingMachineStatePersistenceTests
    {
        [TestMethod]
        public void CanSaveAndRetrieveAMachineState()
        {
            var catalog = new Catalog();
            catalog.ReferenceProduct(ProductsRepository.EvianBottle, new Price(CurrenciesRepository.CHF, 1.1m));
            catalog.ReferenceProduct(ProductsRepository.VittelBottle, new Price(CurrenciesRepository.CHF, 1.2m));
            catalog.ReferenceProduct(ProductsRepository.VolvicBottle, new Price(CurrenciesRepository.CHF, 1.3m));

            var EvianBottles = ItemsFactory.Make(ProductsRepository.EvianBottle, 3);
            var VittelBottles = ItemsFactory.Make(ProductsRepository.VittelBottle, 5);
            var VolvicBottles = ItemsFactory.Make(ProductsRepository.VolvicBottle, 4);
            var allBottles = EvianBottles.Concat(VittelBottles).Concat(VolvicBottles);

            var CHF5Coins = CoinsFactory.Make(CoinsTypesRepository.FiveSwissFrancs, 30);
            var CHF1Coins = CoinsFactory.Make(CoinsTypesRepository.OneSwissFranc, 50);
            var CHF0_20Coins = CoinsFactory.Make(CoinsTypesRepository.TwentySwissFrancCents, 40);
            var allCoins = CHF5Coins.Concat(CHF1Coins).Concat(CHF0_20Coins);

            var inputVendingMachine = new Implementation.VendingMachine(catalog, 20, CoinsTypesRepository.SwissFrancCoins, 100);

            inputVendingMachine.Feed(allBottles);
            inputVendingMachine.Insert(allCoins);

            int stateId;
            using (var repository = new VendingMachinesStatesRepository())
            {
                stateId = repository.SaveStateOf(inputVendingMachine);
            }

            IVendingMachine outputVendingMachine;
            using (var repository = new VendingMachinesStatesRepository())
            {
                var vendingMachineState = repository.GetStateById(stateId);

                outputVendingMachine = VendingMachinesStatesRepository.GetVendingMachineFromState(vendingMachineState);
            }

            Assert.AreEqual(inputVendingMachine, outputVendingMachine);
        }

        [TestMethod]
        public void CanChangeASavedVendingMachineState()
        {
            VendingMachinesStatesRepository.ResetDatabase();

            using (var repository = new VendingMachinesStatesRepository())
            {
                var states = repository.GetAllStates();

                Assert.IsFalse(states.Any());
            }

            var vendingMachine = new LombardOdierVendingMachine(10, 100);
            vendingMachine.Feed(ItemsFactory.Make(ProductsRepository.EvianBottle, 3));
            vendingMachine.Insert(CoinsFactory.Make(CoinsTypesRepository.FiveSwissFrancs, 2));

            string name = Guid.NewGuid().ToString();
            int id;

            using (var repository = new VendingMachinesStatesRepository())
            {
                id = repository.SaveStateOf(vendingMachine, name);
            }

            IVendingMachine intermediateVendingMachine;
            using (var repository = new VendingMachinesStatesRepository())
            {
                var states = repository.GetAllStates();

                Assert.AreEqual(1, states.Count());

                var state = states.Single();
                intermediateVendingMachine = VendingMachinesStatesRepository.GetVendingMachineFromState(state);
            }

            Assert.IsTrue(intermediateVendingMachine.Store.Has(ProductsRepository.EvianBottle));
            Assert.AreEqual(3u, intermediateVendingMachine.Store.NumberOf(ProductsRepository.EvianBottle));
            Assert.AreEqual(10m, intermediateVendingMachine.InsertedAmount);

            intermediateVendingMachine.Feed(ItemsFactory.Make(ProductsRepository.CocaColaBottle, 2));
            intermediateVendingMachine.Insert(CoinsFactory.Make(CoinsTypesRepository.TwoSwissFrancs, 1));

            using (var repository = new VendingMachinesStatesRepository())
            {
                repository.SaveStateOf(intermediateVendingMachine, name, id);
            }

            IVendingMachine finalVendingMachine;
            using (var repository = new VendingMachinesStatesRepository())
            {
                var states = repository.GetAllStates();

                Assert.AreEqual(1, states.Count());

                var state = states.Single();
                finalVendingMachine = VendingMachinesStatesRepository.GetVendingMachineFromState(state);
            }

            Assert.IsTrue(finalVendingMachine.Store.Has(ProductsRepository.EvianBottle));
            Assert.IsTrue(finalVendingMachine.Store.Has(ProductsRepository.CocaColaBottle));
            Assert.AreEqual(3u, finalVendingMachine.Store.NumberOf(ProductsRepository.EvianBottle));
            Assert.AreEqual(2u, finalVendingMachine.Store.NumberOf(ProductsRepository.CocaColaBottle));
            Assert.AreEqual(12m, finalVendingMachine.InsertedAmount);
        }
    }
}
