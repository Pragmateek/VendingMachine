using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Business.Implementation;
using Implementation = VendingMachine.Business.Implementation;
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
            catalog.ReferenceProduct(ProductsRepository.Evian, new Price(CurrenciesRepository.CHF, 1.1m));
            catalog.ReferenceProduct(ProductsRepository.Vittel, new Price(CurrenciesRepository.CHF, 1.2m));
            catalog.ReferenceProduct(ProductsRepository.Volvic, new Price(CurrenciesRepository.CHF, 1.3m));

            var EvianBottles = ItemsFactory.Make(ProductsRepository.Evian, 3);
            var VittelBottles = ItemsFactory.Make(ProductsRepository.Vittel, 5);
            var VolvicBottles = ItemsFactory.Make(ProductsRepository.Volvic, 4);
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

                outputVendingMachine = repository.GetVendingMachineFromState(vendingMachineState);
            }

            Assert.AreEqual(inputVendingMachine, outputVendingMachine);

            //Assert.AreEqual(outputVendingMachine.Catalog.Count(), 3);
            //var EvianCatalogEntry = outputVendingMachine.Catalog.GetEntryFor(ProductsRepository.Evian);
            //Assert.AreEqual(new Price(CurrenciesRepository.CHF, 1.1m), EvianCatalogEntry.Price);

            //var VittelCatalogEntry = outputVendingMachine.Catalog.GetEntryFor(ProductsRepository.Vittel);
            //Assert.AreEqual(new Price(CurrenciesRepository.CHF, 1.2m), VittelCatalogEntry.Price);

            //var VolvicCatalogEntry = outputVendingMachine.Catalog.GetEntryFor(ProductsRepository.Volvic);
            //Assert.AreEqual(new Price(CurrenciesRepository.CHF, 1.3m), VolvicCatalogEntry.Price);


            //var firstSlot = outputVendingMachine.Store.First();
            //Assert.AreEqual(ProductsRepository.Evian, firstSlot.CatalogEntry.Product);
            //Assert.AreEqual(20u, firstSlot.Capacity);
            //Assert.AreEqual(3u, firstSlot.Count);

            //var secondSlot = outputVendingMachine.Store.ElementAt(1);
            //Assert.AreEqual(ProductsRepository.Vittel, secondSlot.CatalogEntry.Product);
            //Assert.AreEqual(20u, secondSlot.Capacity);
            //Assert.AreEqual(5u, secondSlot.Count);

            //var thirdSlot = outputVendingMachine.Store.ElementAt(2);
            //Assert.AreEqual(ProductsRepository.Volvic, thirdSlot.CatalogEntry.Product);
            //Assert.AreEqual(20u, thirdSlot.Capacity);
            //Assert.AreEqual(4u, thirdSlot.Count);


            //var firstCashRegisterSlot = 
        }
    }
}
