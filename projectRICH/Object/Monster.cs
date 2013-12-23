using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object
{
    [Module.Module(typeof(Module.Moving))]
    [Module.Module(typeof(Module.Drawable))]
    public class Monster : GameObject<Monster>
    {
    }
}
