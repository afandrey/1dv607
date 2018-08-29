using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class Display
    {
        public void PrintMessage(string msg)
        {
            Console.WriteLine(msg);
        }
        public void PrintErrorMessage(string errMsg)
        {
            Console.WriteLine(errMsg);
        }
    }
}