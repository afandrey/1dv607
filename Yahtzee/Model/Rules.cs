using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class Rules
    {
        private DiceList diceList;
        private bool ThreeOfAKind()
        {

            if (diceList.GetMaxNumberOfSameValues() >= 3)
            {
                return true;
            }
            return false;
        }
        private bool FourOfAKind()
        {

            if (diceList.GetMaxNumberOfSameValues() >= 4)
            {
                return true;
            }
            return false;
        }
        private int SumOfSameCombo(Combo combo)
        {
            int faceValue = (int)combo + 1;
            int[] diceValue = diceList.GetNumberOfDiceFaceValue();
            return diceValue[faceValue - 1] * (faceValue);
        }
        public bool FullHouse()
        {
            int[] diceVal = diceList.GetNumberOfDiceFaceValue();
            bool retValue = false;
            for (int i = 0; i < diceVal.Length; i++)
            {
                if (diceVal[i] == 2)
                {
                    for (int j = 0; j < diceVal.Length; j++)
                    {
                        if (diceVal[j] == 3)
                            retValue = true;
                    }
                }
            }
            return retValue;
        }
        public bool SmallStraight()
        {
            int[] diceVal = diceList.GetNumberOfDiceFaceValue();
            bool straight = false;
            if (diceVal[0] == 1 || diceVal[0] == 2)
            {
                straight = true;
                for (int i = 1; i < 4; i++)
                {
                    if (diceVal[i] != 1 && diceVal[i] != 2)
                    {
                        straight = false;
                    }
                }

            }
            if (!straight && (diceVal[1] == 1 || diceVal[1] == 2))
            {
                straight = true;
                for (int i = 2; i < diceVal.Length; i++)
                {
                    if (diceVal[i] != 1 && diceVal[i] != 2)
                    {
                        straight = false;
                    }
                }
            }
            if (!straight && (diceVal[2] == 1 || diceVal[2] == 2))
            {
                straight = true;
                for (int i = 3; i < diceVal.Length; i++)
                {
                    if (diceVal[i] != 1 && diceVal[i] != 2)
                    {
                        straight = false;
                    }
                }
            }
            return straight;
        }
        public bool LargeStraight()
        {
            int[] diceValue = diceList.GetNumberOfDiceFaceValue();
            if (diceValue[0] == 1)
            {
                for (int i = 0; i < diceValue.Length - 1; i++)
                {
                    if (diceValue[i] != 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (diceValue[1] == 1)
            {
                for (int i = 1; i < diceValue.Length; i++)
                {
                    if (diceValue[i] != 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public bool Yahtzee()
        {
            int[] diceVal = diceList.GetNumberOfDiceFaceValue();
            bool retVal = false;
            for (int i = 0; i < diceVal.Length; i++)
            {
                if (diceVal[i] == 5)
                    retVal = true;
            }
            return retVal;
        }
        public int CheckCombinations(Combo combo)
        {
            int toReturn = 0;
            switch (combo)
            {
                case Combo.Ones:
                case Combo.Twos:
                case Combo.Threes:
                case Combo.Fours:
                case Combo.Fives:
                case Combo.Sixes:
                    toReturn = SumOfSameCombo(combo);
                    break;
                case Combo.ThreeOfAKind:
                    if (ThreeOfAKind())
                        toReturn = diceList.GetSum();
                    break;
                case Combo.FourOfAKind:
                    if (FourOfAKind())
                        toReturn = diceList.GetSum();
                    break;
                case Combo.FullHouse:
                    if (FullHouse())
                        toReturn = 25;
                    break;
                case Combo.SmallStraight:
                    if (SmallStraight())
                        toReturn = 30;
                    break;
                case Combo.LargeStraight:
                    if (LargeStraight())
                        toReturn = 40;
                    break;
                case Combo.Yahtzee:
                    if (Yahtzee())
                        toReturn = 50;
                    break;
                case Combo.Chance:
                    toReturn = diceList.GetSum();
                    break;
            }
            return toReturn;
        }
        public Rules(DiceList diceList)
        {
            this.diceList = diceList;
        }
    }
}