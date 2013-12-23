using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object.Module
{
    class Drawable : IModule
    {
        private Render.IRender render;
        public Drawable()
        {

        }

        [ModuleCommand("Stop")]
        public void Stop(IGameObject owner)
        {
            
        }

        [ModuleCommand("Show")]
        public void Show(IGameObject owner)
        {
            var renderer = new Render.ShapeRenderer<Render.Shape.Circle>(new Render.Shape.Circle());
            renderer.TransformCallback = new Action<System.Drawing.Drawing2D.Matrix>((transform) =>
            {
                var position = owner.Execute<Entity.Vector>("GetCurrentPosition");
                transform.Translate(position.X, position.Y);
            });
            render = renderer;
            World.RenderManager.Regist(render);
        }

        [ModuleCommand("Hide")]
        public void Hide(IGameObject owner)
        {
            World.RenderManager.Unregist(render);
            render = null;
        }
    }
}
