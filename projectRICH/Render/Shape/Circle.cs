using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Render.Shape
{
    class Circle : IShape
    {
        public void Draw(System.Drawing.Graphics g)
        {
            g.FillEllipse(System.Drawing.SystemBrushes.Window, 0, 0, 100, 100);
        }
    }
}
