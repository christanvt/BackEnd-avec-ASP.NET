using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilisationClasse
{
    class Program
    {
        static void Main(string[] args)
        {
            Point pointA = new Point();
            pointA.X = 1;
            pointA.Y = 1;

            Console.WriteLine($"Point A , x = {pointA.X}, Y={pointA.Y}");

            Point pointB = new Point { Y = 2, X = 2 };
            Console.WriteLine($"Point B , x = {pointB.X}, Y={pointB.Y}");

            pointA.Ajouter(pointB);

            Console.WriteLine($"Point A  après modifications, x = {pointA.X}, Y={pointA.Y}");
            Console.ReadLine();

        }
    }
}
