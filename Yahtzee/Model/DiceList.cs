using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class DiceList
    {
        public const int NoOfDice = 5;
        public List<Dice> Dice { get; private set; }
        public void Roll(bool[] diceToRoll)
        {
            foreach (Dice d in Dice)
            {
                if (diceToRoll[d.Id-1])
                {
                    d.Roll();
                }
            }
        }
        public int[] GetDice()
        {
            int[] diceValues = { 0, 0, 0, 0, 0 };
            int index = 0;
            foreach (Dice d in Dice)
            {
                diceValues[index++] = d.Value;
            }
            return diceValues;
        }
        public int[] GetNumberOfDiceFaceValue()
        {
            int[] diceValues = { 0, 0, 0, 0, 0, 0 };
            foreach (Dice d in Dice)
            {
                diceValues[d.Value - 1]++;
            }
            return diceValues;
        }
        public int GetMaxNumberOfSameValues()
        {
            int[] diceValues = GetNumberOfDiceFaceValue();
            int highestNumberOfSame = 0;
            for (int i = 0; i < 6; i++)
            {
                if (diceValues[i] > highestNumberOfSame)
                    highestNumberOfSame = diceValues[i];
            }
            return highestNumberOfSame;
        }
        public int GetSum()
        {
            int sum = 0;
            foreach (Dice d in Dice)
            {
                sum += d.Value;
            }
            return sum;
        }
        public DiceList()
        {
            Dice = new List<Dice>();
            for (int i = 1; i <= NoOfDice; i++)
            {
                Thread.Sleep(20);
                Dice.Add(new Dice(i));
            }
        }
    }
}