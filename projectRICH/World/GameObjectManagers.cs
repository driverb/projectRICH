using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.World
{
    public class GameObjectManagers
    {
        private static RenderManager renders = new RenderManager();

        public static Object.Monster Player = new Object.Monster();
        public static RenderManager Renders { get { return renders; } }
    }
}
