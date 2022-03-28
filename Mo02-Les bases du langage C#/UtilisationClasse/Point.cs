using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilisationClasse
{
   public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Ajouter(Point point)
        {
            this.X += point.X;
            this.Y += point.Y;
        }
    }
}
