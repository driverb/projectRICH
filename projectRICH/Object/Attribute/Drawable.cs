using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object.Attribute
{
    class Drawable : Attribute.IAttribute
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
