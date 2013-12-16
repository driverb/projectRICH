using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object.Module
{
    [AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    class ModuleCommand : System.Attribute
    {
        private string name;

        public string Name { get { return name; } }
        public ModuleCommand(string name)
        {
            this.name = name;
        }
    }
}
