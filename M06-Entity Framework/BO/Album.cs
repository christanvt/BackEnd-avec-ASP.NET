using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Album
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Groupe { get; set; }
        public int Annee{get;set;}
        public int NombreDePistes { get; set; }

    }
}
