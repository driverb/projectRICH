using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object.Attribute
{
    [AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    class AttributeCommand : System.Attribute
    {
        private string name;

        public string Name { get { return name; } }
        public AttributeCommand(string name)
        {
            this.name = name;
        }
    }
}
