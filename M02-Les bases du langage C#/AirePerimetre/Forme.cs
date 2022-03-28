using System;


namespace AirePerimtre
{
    public abstract class Forme
    {
        public abstract double Aire
        {
            get;
        }
        public abstract double Perimetre
        {
            get;
        }
        public override string ToString()
        {
            return $" Aire = {Aire}"+Environment.NewLine+$" Pérmètre = {Perimetre}"+Environment.NewLine;
        }
    }
}
