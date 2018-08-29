using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Game g = new Game();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }
    }
}
