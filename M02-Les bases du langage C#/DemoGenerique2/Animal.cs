﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGenerique2
{
   public interface Animal<T> where T:Nourriture
    {
        bool SeNourrir(T aliment);
    }
}
