using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGenerique2
{
    class Fruit:Nourriture
    {
        public DateTime DateCueilette { get; set; }
        public bool EstPerime()
        {
            return (DateTime.Now - DateCueilette).Days > 10;
        }
    }
}
