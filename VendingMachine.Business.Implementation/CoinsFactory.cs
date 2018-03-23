using System;
using System.Collections.Generic;
using VendingMachine.Business.Contracts;

namespace VendingMachine.Business.Implementation
{
    public class CoinsFactory
    {
        public static ICoin Make(ICoinType coinType)
        {
            var coin = new Coin(coinType);

            return coin;
        }

        public static IEnumerable<ICoin> Make(ICoinType coinType, uint quantity)
        {
            var coins = new List<ICoin>();

            for (uint i = 1; i <= quantity; i++)
            {
                var coin = Make(coinType);

                coins.Add(coin);
            }

            return coins;
        }
    }
}
