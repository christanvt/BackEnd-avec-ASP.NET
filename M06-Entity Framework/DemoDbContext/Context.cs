﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDbContext
{
   public class Context:DbContext
    {
        public DbSet<Personne> Personnes { get; set; }
    }
}
