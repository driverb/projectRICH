using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Render
{
    class ShapeRenderer<T> : IRender where T : class, Shape.IShape
    {
        private System.Drawing.Rectangle boundary;
        private T shape;
        private System.Drawing.Drawing2D.Matrix transform = new System.Drawing.Drawing2D.Matrix();
        public Action<System.Drawing.Drawing2D.Matrix> TransformCallback { get; set; }

        public ShapeRenderer(T shape)
        {
            this.shape = shape;
        }

        public void OnRender(System.Drawing.Graphics g)
        {
            if (TransformCallback != null)
            {
                transform.Reset();
                TransformCallback(transform);
            }

            g.Transform = transform;
            shape.Draw(g);
        }

        public System.Drawing.Rectangle Boundary
        {
            get 
            { 
                return boundary; 
            }
            set 
            {
                transform.Translate(value.Left, value.Top);
                boundary = value;
            }
        }
    }
}
