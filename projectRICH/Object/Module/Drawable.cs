using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object.Module
{
    class Drawable : Module.IModule
    {
        
        private Render.IRender render;
        public Drawable()
        {
        }

        public void OnUpdate(long currentTime)
        {

            throw new NotImplementedException();
        }
    }
}
