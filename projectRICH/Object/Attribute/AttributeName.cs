using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object.Attribute
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    class AttributeName : System.Attribute
    {
        private string name;

        public AttributeName(string name)
        {
            this.name = name;
        }
    }
}
