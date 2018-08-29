using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class ComboModel
    {
        public static string GetName(Combo combo) => Enum.GetName(typeof(Combo), combo);

        public static int GetSize() => Enum.GetNames(typeof(Combo)).Length;
        public static Combo GetCombo(int index) => (Combo)index;

        public static Array GetList() => Enum.GetValues(typeof(Combo));
    }
}