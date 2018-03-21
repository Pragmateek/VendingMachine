using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Data;
using VendingMachine.Business.Implementation;

namespace VendingMachine.Tests
{
    [TestClass]
    public class CashRegisterTests
    {
        private ICashRegister GetACashRegisterWith90SwissFrancs()
        {
            var cashRegister = new CashRegister(CoinsTypesRepository.SwissFrancCoins, 20);

            var fiftyFrancsInFiveFrancsCoins = CoinsFactory.Get(CoinsTypesRepository.FiveSwissFrancs, 10);
            var thirtyFrancsInTwoFrancsCoins = CoinsFactory.Get(CoinsTypesRepository.TwoSwissFrancs, 15);
            var tenFrancsInOneFrancCoins = CoinsFactory.Get(CoinsTypesRepository.OneSwissFranc, 10);

            var allCoins = fiftyFrancsInFiveFrancsCoins.Concat(thirtyFrancsInTwoFrancsCoins).Concat(tenFrancsInOneFrancCoins);

            cashRegister.TryPut(allCoins);

            return cashRegister;
        }


        [TestMethod]
        public void CanPutDifferentCoinsInACashRegister()
        {
            var cashRegister = new CashRegister(CoinsTypesRepository.SwissFrancCoins, 20);

            var fiftyFrancsInFiveFrancsCoins = CoinsFactory.Get(CoinsTypesRepository.FiveSwissFrancs, 10);
            var thirtyFrancsInTwoFrancsCoins = CoinsFactory.Get(CoinsTypesRepository.TwoSwissFrancs, 15);
            var tenFrancsInOneFrancCoins = CoinsFactory.Get(CoinsTypesRepository.OneSwissFranc, 10);

            var allCoins = fiftyFrancsInFiveFrancsCoins.Concat(thirtyFrancsInTwoFrancsCoins).Concat(tenFrancsInOneFrancCoins);

            var canPutMoney = cashRegister.TryPut(allCoins);

            Assert.IsTrue(canPutMoney);
            Assert.AreEqual(90, cashRegister.Amount);
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
            var allCoins = CoinsTypesRepository.SwissFrancCoins.SelectMany(coinType => CoinsFactory.Get(coinType, 10));
            cashRegister.TryPut(allCoins);

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
    }
}
