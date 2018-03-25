using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Contracts;
using VendingMachine.Tools;

namespace VendingMachine.Business.Implementation
{
    public class CashRegister : ICashRegister
    {
        private readonly ICashRegisterSlot[] slots;
        public IEnumerable<ICashRegisterSlot> Slots => slots;

        public decimal Amount => slots.Sum(slot => slot.Amount);

        public CashRegister(IEnumerable<ICoinType> coinTypes, uint capacity)
        {
            slots = coinTypes.Select(coinType => new CashRegisterSlot(coinType, capacity)).ToArray();
        }

        public bool TryPut(ICoin coin)
        {
            var slot = Slots.FirstOrDefault(s => s.CoinType == coin.Type);

            if (slot == null)
            {
                throw new Exception($"This cash register has no slot for '{coin.Type}'!");
            }

            return slot.TryPut(coin);
        }

        public void Put(IEnumerable<ICoin> coins)
        {
            foreach (var coin in coins)
            {
                TryPut(coin);
            }
        }

        public bool CanGetChangeOn(decimal amount)
        {
            IEnumerable<ICoin> coins;
            return TryGetInternal(Slots, amount, out coins);
        }

        private bool TryGetInternal(IEnumerable<ICashRegisterSlot> slots, decimal amount, out IEnumerable<ICoin> coins)
        {
            coins = Enumerable.Empty<ICoin>();

            // Useless to check slots whose coins value is above amount.
            var candidateSlots = slots.Where(slot => slot.Count != 0 && slot.CoinType.FaceValue <= amount);

            if (!candidateSlots.Any())
            {
                // The amount is too small for the available slots.
                return false;
            }

            var biggestInferiorSlotValue = candidateSlots.Max(slot => slot.CoinType.FaceValue);
            var biggestInferiorSlot = candidateSlots.Single(slot => slot.CoinType.FaceValue == biggestInferiorSlotValue);

            var haveFoundASolution = false;
            for (uint n = biggestInferiorSlot.Count; n >= 1; n--)
            {
                // How many n coins of this slot are worth?
                var stackValue = n * biggestInferiorSlotValue;

                if (stackValue > amount)
                {
                    continue;
                }

                var remainder = amount - stackValue;

                if (remainder == 0)
                {
                    // The amount is a multiple of current slot coins value.
                    haveFoundASolution = true;

                    coins = CoinsFactory.Make(biggestInferiorSlot.CoinType, n);

                    break;
                }

                var lowerSlots = candidateSlots.Except(new[] { biggestInferiorSlot });

                IEnumerable<ICoin> remainderCoins;

                if (!TryGetInternal(lowerSlots, remainder, out remainderCoins))
                {
                    continue;
                }

                // We have found a solution for the remainder, hence for the whole amount
                haveFoundASolution = true;

                var currentSlotCoins = CoinsFactory.Make(biggestInferiorSlot.CoinType, n);

                coins = remainderCoins.Concat(currentSlotCoins);

                break;
            }

            return haveFoundASolution;
        }

        public bool TryGetChange(decimal amount, out IEnumerable<ICoin> coins)
        {
            var canGetChange = TryGetInternal(Slots, amount, out coins);

            if (canGetChange)
            {
                Remove(coins);
            }

            return canGetChange;
        }

        public void Remove(IEnumerable<ICoin> coins)
        {
            foreach (var coin in coins)
            {
                var slot = Slots.FirstOrDefault(s => s.CoinType == coin.Type);

                slot.Remove(coin);
            }
        }

        public override bool Equals(object obj)
        {
            var otherCashRegister = obj as CashRegister;

            return otherCashRegister != null && otherCashRegister.Slots.SequenceEqual(Slots);
        }

        public override int GetHashCode()
        {
            return slots.GetElementsHashCode();
        }
    }
}