﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Data;
using VendingMachine.Business.Implementation;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Tests
{
    [TestClass]
    public class CashRegisterTests
    {
        private ICashRegister GetACashRegisterWith90SwissFrancs()
        {
            var cashRegister = new CashRegister(CoinsTypesRepository.SwissFrancCoins, 20);

            var fiftyFrancsInFiveFrancsCoins = CoinsFactory.Make(CoinsTypesRepository.FiveSwissFrancs, 10);
            var thirtyFrancsInTwoFrancsCoins = CoinsFactory.Make(CoinsTypesRepository.TwoSwissFrancs, 15);
            var tenFrancsInOneFrancCoins = CoinsFactory.Make(CoinsTypesRepository.OneSwissFranc, 10);

            var allCoins = fiftyFrancsInFiveFrancsCoins.Concat(thirtyFrancsInTwoFrancsCoins).Concat(tenFrancsInOneFrancCoins);

            cashRegister.Put(allCoins);

            return cashRegister;
        }


        [TestMethod]
        public void CanPutDifferentCoinsInACashRegister()
        {
            var cashRegister = new CashRegister(CoinsTypesRepository.SwissFrancCoins, 20);

            var fiftyFrancsInFiveFrancsCoins = CoinsFactory.Make(CoinsTypesRepository.FiveSwissFrancs, 10);
            var thirtyFrancsInTwoFrancsCoins = CoinsFactory.Make(CoinsTypesRepository.TwoSwissFrancs, 15);
            var tenFrancsInOneFrancCoins = CoinsFactory.Make(CoinsTypesRepository.OneSwissFranc, 10);

            var allCoins = fiftyFrancsInFiveFrancsCoins.Concat(thirtyFrancsInTwoFrancsCoins).Concat(tenFrancsInOneFrancCoins);

            cashRegister.Put(allCoins);
            //var canPutMoney = cashRegister.Put(allCoins);

            //Assert.IsTrue(canPutMoney);
            Assert.AreEqual(90, cashRegister.Amount);
        }

        [TestMethod]
        public void CannotPutMoreCoinsThanTheCashRegisterCapacity()
        {
            var cashRegister = new CashRegister(CoinsTypesRepository.SwissFrancCoins, 10);

            for (var i = 1; i <= 20; i++)
            {
                var fiveSwissFrancs = CoinsFactory.Make(CoinsTypesRepository.FiveSwissFrancs);

                var coinHasBeenPut = cashRegister.TryPut(fiveSwissFrancs);

                if (i <= 10)
                {
                    Assert.IsTrue(coinHasBeenPut);
                    Assert.AreEqual(5m * i, cashRegister.Amount);
                }
                else
                {
                    Assert.IsFalse(coinHasBeenPut);
                    Assert.AreEqual(50m, cashRegister.Amount);
                }
            }
        }

        [TestMethod]
        public void CannotPutTheSameCoinTwice()
        {
            var cashRegister = new CashRegister(CoinsTypesRepository.SwissFrancCoins, 10);

            var fiveSwissFrancs = CoinsFactory.Make(CoinsTypesRepository.FiveSwissFrancs);

            var canPutOnce = cashRegister.TryPut(fiveSwissFrancs);
            Assert.IsTrue(canPutOnce);

            var canPutTwice = cashRegister.TryPut(fiveSwissFrancs);
            Assert.IsFalse(canPutTwice);
        }

        [TestMethod]
        public void CannotGetAnythingFromAnEmptyCashRegister()
        {
            var cashRegister = new CashRegister(CoinsTypesRepository.SwissFrancCoins, 10);

            IEnumerable<ICoin> coins;

            var canGetNothing = cashRegister.TryGetChange(0, out coins);
            Assert.IsFalse(canGetNothing);
            Assert.IsNotNull(coins);
            Assert.AreEqual(0, coins.Count());

            var canGetOneSwissFranc = cashRegister.TryGetChange(1, out coins);
            Assert.IsFalse(canGetOneSwissFranc);
        }

        [TestMethod]
        public void CannotGetChangeIfCashRegisterDoesNotHaveEnoughCash()
        {
            var cashRegister = GetACashRegisterWith90SwissFrancs();

            IEnumerable<ICoin> coins;

            var canGet100SwissFrancs = cashRegister.TryGetChange(100, out coins);
            Assert.IsFalse(canGet100SwissFrancs);
        }

        [TestMethod]
        public void CanGetChangeFromManyCoinsTypes()
        {
            var cashRegister = GetACashRegisterWith90SwissFrancs();

            IEnumerable<ICoin> coins;

            var canGet18SwissFrancs = cashRegister.TryGetChange(18, out coins);
            Assert.IsTrue(canGet18SwissFrancs);

            // We should get three 5 CHF coins (15), one 2 CHF coin (2), and one 1 CHF coin (1)
            Assert.AreEqual(5, coins.Count());
            Assert.AreEqual(3, coins.Count(coin => coin.Type == CoinsTypesRepository.FiveSwissFrancs));
            Assert.AreEqual(1, coins.Count(coin => coin.Type == CoinsTypesRepository.TwoSwissFrancs));
            Assert.AreEqual(1, coins.Count(coin => coin.Type == CoinsTypesRepository.OneSwissFranc));
        }

        [TestMethod]
        public void CanGetChangeFromManyCoinsTypesWithCents()
        {
            var cashRegister = new CashRegister(CoinsTypesRepository.SwissFrancCoins, 20);

            // Get 10 coins of each type
            var allCoins = CoinsTypesRepository.SwissFrancCoins.SelectMany(coinType => CoinsFactory.Make(coinType, 10));
            cashRegister.Put(allCoins);

            IEnumerable<ICoin> coins;
            // Should take 1 coin of each type
            var canGet8SwissFrancsAnd85Cents = cashRegister.TryGetChange(8.85m, out coins);
            Assert.IsTrue(canGet8SwissFrancsAnd85Cents);

            Assert.AreEqual(CoinsTypesRepository.SwissFrancCoins.Count(), coins.Count());
            foreach(var coinType in CoinsTypesRepository.SwissFrancCoins)
            {
                Assert.AreEqual(1, coins.Count(coin => coin.Type == coinType));
            }
        }

        [TestMethod]
        public void ShouldRemoveCoinsFromRegisterWhenChangeIsGivenBack()
        {
            var cashRegister = new CashRegister(CoinsTypesRepository.SwissFrancCoins, 10);

            var coins = CoinsFactory.Make(CoinsTypesRepository.OneSwissFranc, 3);
            cashRegister.Put(coins);

            IEnumerable<ICoin> changeCoins;
            bool canGetChange = cashRegister.TryGetChange(1m, out changeCoins);

            Assert.IsTrue(canGetChange);
            Assert.AreEqual(1, changeCoins.Count());
            Assert.AreEqual(1m, changeCoins.Single().Type.FaceValue);
            Assert.AreEqual(2m, cashRegister.Amount);
        }
    }
}
