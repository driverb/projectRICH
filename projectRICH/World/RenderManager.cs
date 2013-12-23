using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.World
{
    public class RenderManager
    {
        private static List<Render.IRender> renders = new List<Render.IRender>();
        public static void Regist(Render.IRender renderable)
        {
            // QTree로 빼야할 듯.
            renders.Add(renderable);
        }

        public static void Unregist(Render.IRender renderable)
        {
            renders.Remove(renderable);
        }

        public static void Render(System.Drawing.Graphics view)
        {
            foreach (var r in renders)
            {
                r.OnRender(view);
            }
        }
    }
}
