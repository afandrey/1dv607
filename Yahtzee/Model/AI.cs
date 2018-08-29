using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class AI : Player
    {
        private Rules rules;
        private bool[] DiceToRoll { get; set; }
        public bool[] GetDiceToRoll(int[] diceVal, int[] dice)
        {
            DiceToRoll = new bool[] { false, false, false, false, false };

            if (Stand()) ;
            else if (KeepSomeOfAKind(diceVal, dice)) ;
            else if (KeepStraight(diceVal, dice)) ;
            else if (KeepTwoPair(diceVal, dice)) ;
            else if (KeepPair(diceVal, dice)) ;
            else
            {
                DiceToRoll = new bool[] { true, true, true, true, true };
            }
            return DiceToRoll;
        }
        public Combo SelectComboToUse()
        {
            int highestValue = 0;
            Combo highCombo = 0;
            int[] getValueForCombos = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (Combo c in ComboModel.GetList())
            {
                int i = (int)c;
                getValueForCombos[i] = rules.CheckCombinations(c);
                if (c != Combo.Chance && !GetUsedCombo(c) && getValueForCombos[i] >= highestValue)
                {
                    highestValue = getValueForCombos[i];
                    highCombo = c;
                }
            }
            getValueForCombos[12] = rules.CheckCombinations(Combo.Chance);
            if (!GetUsedCombo(Combo.Chance) && getValueForCombos[12] > highestValue && highestValue < 10 && highCombo > Combo.Threes && highCombo < Combo.Yahtzee)
            {
                highCombo = Combo.Chance;
            }
            return highCombo;
        }
        private bool Stand()
        {
            if ((rules.Yahtzee()) && !GetUsedCombo(Combo.Yahtzee) ||
                (rules.FullHouse()) && !GetUsedCombo(Combo.FullHouse) ||
                (rules.LargeStraight()) && !GetUsedCombo(Combo.LargeStraight) ||
                (rules.SmallStraight()) && !GetUsedCombo(Combo.SmallStraight))
            {
                return true;
            }
            return false;
        }
        private bool KeepSomeOfAKind(int[] diceVal, int[] dice)
        {
            for (int i = 0; i < diceVal.Length; i++)
            {
                if ((diceVal[i] == 4) || (diceVal[i] == 3))
                {
                    for (int j = 0; j < dice.Length; j++)
                    {
                        if (dice[j] != i + 1)
                            DiceToRoll[j] = true;
                    }
                    return true;
                }
            }
            return false;
        }
        private bool KeepStraight(int[] diceVal, int[] dice)
        {
            if (!(GetUsedCombo(Combo.SmallStraight) && GetUsedCombo(Combo.LargeStraight)))
            {
                for (int i = 5; i > 2; i--)
                {
                    if ((diceVal[i] > 0) && (diceVal[i - 1] > 0) && (diceVal[i - 2] > 0))
                    {
                        DiceToRoll = new bool[] { true, true, true, true, true };
                        for (int j = 0; j < dice.Length; j++)
                        {
                            if (dice[j] == i + 1)
                            {
                                DiceToRoll[j] = false;
                                for (int k = 0; k < dice.Length; k++)
                                {
                                    if (dice[k] == i)
                                    {
                                        DiceToRoll[k] = false;
                                        for (int m = 0; m < dice.Length; m++)
                                        {
                                            if (dice[m] == i - 1)
                                            {
                                                DiceToRoll[m] = false;
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            return false;
        }
        private bool KeepTwoPair(int[] diceVal, int[] dice)
        {
            if (!GetUsedCombo(Combo.FullHouse))
            {
                int firstPairValue = 0;
                int secondPairValue = 0;
                for (int i = 0; i < diceVal.Length; i++)
                {
                    if (diceVal[i] == 2)
                    {
                        firstPairValue = i + 1;
                        for (int j = 0; j < diceVal.Length; j++)
                        {
                            if ((diceVal[j] == 2) && (j + 1 != firstPairValue))
                            {
                                for (int k = 0; k < dice.Length; k++)
                                {
                                    secondPairValue = j + 1;
                                    if ((dice[k] != firstPairValue) && (dice[k] != secondPairValue))
                                    {
                                        DiceToRoll[k] = true;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        private bool KeepPair(int[] diceVal, int[] dice)
        {
            for (int i = 2; i < diceVal.Length; i++)
            {
                if (diceVal[i] == 2)
                {
                    for (int j = 0; j < dice.Length; j++)
                    {
                        if (dice[j] != i + 1)
                            DiceToRoll[j] = true;
                    }
                    return true;
                }
            }
            return false;
        }
        public AI(int id, Rules rules): base("AI" + id, true)
        {
            this.rules = rules;
        }
        public AI(string name, Rules rules, List<Score> scores): base(name, scores, true)
        {
            this.rules = rules;
        }
    }
}