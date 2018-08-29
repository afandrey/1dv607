using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class Dice
    {
        private Random random;
        public int Id { get; private set; }
        public int Value { get; private set; }
        public void Roll()
        {
            Value = random.Next(1, 7);
        }
        public Dice(int id)
        {
            random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            Id = id;
            Value = 0;
        }
    }
}