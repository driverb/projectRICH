﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object.Module
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    class ModuleName : System.Attribute
    {
        private string name;

        public ModuleName(string name)
        {
            this.name = name;
        }
    }
}
