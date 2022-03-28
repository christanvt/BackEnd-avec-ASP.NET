using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGenerique2
{
    public class Chat : Animal<Pate>
    {
        public bool SeNourrir(Pate aliment)
        {
            if (aliment.EstPerime())
            {
                return false;
            }
            Console.WriteLine("Le chat mange le délicieux paté");
            return true;
        }
    }
}
