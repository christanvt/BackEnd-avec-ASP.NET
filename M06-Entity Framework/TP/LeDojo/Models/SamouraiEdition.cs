using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeDojo.Models
{
    public class SamouraiEdition
    {
  
            public Samourai Samourai { get; set; }
            public List<Arme> Armes { get; set; } 

            public int? IdSelectedArme { get; set; }
       
    }
}