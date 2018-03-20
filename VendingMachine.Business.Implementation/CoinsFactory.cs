using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Business;

namespace VendingMachine.Implementation
{
    public class CoinsFactory
    {
        public static IEnumerable<ICoin> Get(ICoinType coinType, uint count)
        {
            var coins = new List<ICoin>();

            for (uint i = 1; i <= count; i++)
            {
                var coin = new Coin(coinType);

                coins.Add(coin);
            }

            return coins;
        }
    }
}
