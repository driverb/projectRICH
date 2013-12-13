using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.World
{
    public class Ground
    {
        public System.Drawing.Rectangle Window { get; set; }
        private List<Render.IRender> renders = new List<Render.IRender>();
        public void Regist(Render.IRender renderable)
        {
            // QTree로 빼야할 듯.
            renders.Add(renderable);
        }

        public void Unregist(Render.IRender renderable)
        {
            renders.Remove(renderable);
        }

        public void Render(System.Drawing.Graphics view)
        {
            foreach (var r in renders)
            {
                r.OnRender(view);
            }
        }
    }
}
