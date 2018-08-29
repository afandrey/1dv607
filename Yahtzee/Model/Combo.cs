using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    public enum Combo
    {
        Ones, 
        Twos, 
        Threes, 
        Fours, 
        Fives, 
        Sixes, 
        ThreeOfAKind, 
        FourOfAKind, 
        FullHouse, 
        SmallStraight, 
        LargeStraight, 
        Yahtzee, 
        Chance
    }
}