using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object.Module
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    class ModuleAttribute : Attribute
    {
        private Type module;

        public ModuleAttribute(Type module)
        {
            this.module = module;
        }

        public Type Module { get { return this.module; } }
    }

}
