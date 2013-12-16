using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object.Module
{
    class ModuleCollection
    {
        private List<IModule> attributes;

        public T Find<T>() where T : class, IModule
        {
            foreach (var attr in attributes)
            {
                if (typeof(T).IsInstanceOfType(attr))
                {
                    return attr as T;
                }
            }

            return null;
        }

        public IModule Find(Type t)
        {
            foreach (var attr in attributes)
            {
                if (t.IsInstanceOfType(attr))
                {
                    return attr;
                }
            }

            return null;
        }



    }
}
