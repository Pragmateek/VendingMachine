using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Data;

namespace VendingMachine.Tests
{
    [TestClass]
    public class PricesExtensionsTests
    {
        [TestMethod]
        public void CanFluentlyCreateAPriceInCHF()
        {
            var price = 10.CHF();

            Assert.AreEqual(CurrenciesRepository.CHF, price.Currency);
            Assert.AreEqual(10m, price.Amount);
        }
    }
}
