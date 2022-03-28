using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGenerique2
{
    public class Fourmi : Animal<Fruit>
    {
        bool Animal<Fruit>.SeNourrir(Fruit aliment)
        {
            if (aliment.EstPerime())
            {
                return false;
            }
            Console.WriteLine("La fourmi mange un fruit");
            return true;
        }
    }
}
