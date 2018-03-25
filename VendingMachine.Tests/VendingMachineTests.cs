using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Business.Contracts;
using VendingMachine.Business.Implementation;
using VendingMachine.Data;

namespace VendingMachine.Tests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void CannotBuyAnythingFromAnEmptyMachine()
        {
            var vendingMachine = new LombardOdierVendingMachine(10, 100);

            IItem EvianBottle;
            var canBuySomethingWithoutMoney = vendingMachine.TryBuyItem(ProductsRepository.Evian, out EvianBottle);

            Assert.IsFalse(canBuySomethingWithoutMoney);

            vendingMachine.Insert(CoinsFactory.Make(CoinsTypesRepository.FiveSwissFrancs, 1));

            var canBuySomethingWithMoney = vendingMachine.TryBuyItem(ProductsRepository.Evian, out EvianBottle);

            Assert.IsFalse(canBuySomethingWithMoney);
        }

        [TestMethod]
        public void CanBuySomethingOnceItemsAndEnoughMoneyHaveBeenProvided()
        {
            var vendingMachine = new LombardOdierVendingMachine(10, 100);

            vendingMachine.Feed(ItemsFactory.Make(ProductsRepository.Evian, 2));
            vendingMachine.Insert(CoinsFactory.Make(CoinsTypesRepository.OneSwissFranc));

            IItem EvianBottle;
            var canBuySomethingWithoutEnoughMoney = vendingMachine.TryBuyItem(ProductsRepository.Evian, out EvianBottle);

            Assert.IsFalse(canBuySomethingWithoutEnoughMoney);
            Assert.IsNull(EvianBottle);

            vendingMachine.Insert(CoinsFactory.Make(CoinsTypesRepository.TwentySwissFrancCents));

            var canBuySomethingWithEnoughMoney = vendingMachine.TryBuyItem(ProductsRepository.Evian, out EvianBottle);
            Assert.IsTrue(canBuySomethingWithEnoughMoney);
            Assert.IsNotNull(EvianBottle);
            Assert.AreEqual(0.1m, vendingMachine.InsertedAmount);

            var canBuySomethingWith10Cents = vendingMachine.TryBuyItem(ProductsRepository.Evian, out EvianBottle);
            Assert.IsFalse(canBuySomethingWith10Cents);
            Assert.IsNull(EvianBottle);
        }

        [TestMethod]
        public void ShouldReturnTheFirstFedItemForAGivenProduct()
        {
            var vendingMachine = new LombardOdierVendingMachine(10, 100);

            var firstEvianBottle = ItemsFactory.Make(ProductsRepository.Evian);
            var secondEvianBottle = ItemsFactory.Make(ProductsRepository.Evian);
            var thirdEvianBottle = ItemsFactory.Make(ProductsRepository.Evian);

            vendingMachine.Feed(new[] { firstEvianBottle, secondEvianBottle, thirdEvianBottle });

            vendingMachine.Insert(CoinsFactory.Make(CoinsTypesRepository.FiveSwissFrancs));

            IItem EvianBottle;
            var canGetSomeEvianBottle = vendingMachine.TryBuyItem(ProductsRepository.Evian, out EvianBottle);

            Assert.IsTrue(canGetSomeEvianBottle);
            Assert.IsNotNull(EvianBottle);
            Assert.AreSame(firstEvianBottle, EvianBottle);
        }

        [TestMethod]
        public void CanExhaustVendingMachineStore()
        {
            var vendingMachine = new LombardOdierVendingMachine(10, 100);

            vendingMachine.Feed(ItemsFactory.Make(ProductsRepository.Evian, 3));
            vendingMachine.Insert(CoinsFactory.Make(CoinsTypesRepository.FiveSwissFrancs));

            for (int i = 1; i <= 4; i++)
            {
                IItem EvianBottle;
                var canBuyEvianBottle = vendingMachine.TryBuyItem(ProductsRepository.Evian, out EvianBottle);

                if (i <= 3)
                {
                    Assert.IsTrue(canBuyEvianBottle);
                    Assert.IsNotNull(EvianBottle);
                    Assert.AreEqual(5m - 1.1m * i, vendingMachine.InsertedAmount);
                }
                else if (i == 4)
                {
                    Assert.IsFalse(canBuyEvianBottle);
                    Assert.IsNull(EvianBottle);
                    Assert.AreEqual(5m - 1.1m * 3, vendingMachine.InsertedAmount);
                }
            }
        }

        [TestMethod]
        public void CanGetItemsFromAFedMachine()
        {
            var vendingMachine = new LombardOdierVendingMachine(10, 100);

            vendingMachine.Feed(ItemsFactory.Make(ProductsRepository.Evian, 3));
            vendingMachine.Feed(ItemsFactory.Make(ProductsRepository.CocaCola, 3));
            vendingMachine.Feed(ItemsFactory.Make(ProductsRepository.Fanta, 3));

            vendingMachine.Insert(CoinsFactory.Make(CoinsTypesRepository.FiveSwissFrancs));

            IItem EvianBottle;
            var canBuyAnEvianBottle = vendingMachine.TryBuyItem(ProductsRepository.Evian, out EvianBottle);

            Assert.IsTrue(canBuyAnEvianBottle);
            Assert.IsNotNull(EvianBottle);
            Assert.AreEqual(5m - 1.1m, vendingMachine.InsertedAmount);
        }
    }
}
