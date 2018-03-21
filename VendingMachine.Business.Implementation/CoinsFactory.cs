using System;
using System.Collections.Generic;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class CoinsFactory
    {
        public static IEnumerable<ICoin> Get(ICoinType coinType, uint quantity)
        {
            var coins = new List<ICoin>();

            for (uint i = 1; i <= quantity; i++)
            {
                var coin = new Coin(coinType);

                coins.Add(coin);
            }

            return coins;
        }
    }
}
