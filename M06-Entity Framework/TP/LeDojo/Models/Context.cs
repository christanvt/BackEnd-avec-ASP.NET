using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LeDojo.Models
{
    public class Context:DbContext
    {
        public Context() : base("name=Context")
        {

        }
        public System.Data.Entity.DbSet<BO.Samourai> Samourais { get; set; }
        public System.Data.Entity.DbSet<BO.Arme> Armes { get; set; }
    }
}