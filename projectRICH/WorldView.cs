using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectRICH
{
    public partial class WorldView : Control
    {
        private static World.Ground ground = new World.Ground();
        public static World.Ground GroundInstance
        {
            get { return ground; }
            set { ground = value; }
        }

        private Graphics intersectGraphic;
        private Image intersectBackImage;
        private volatile uint size;

        public WorldView()
        {
            InitializeComponent();
            var backBuffer = CreateGraphics();
            
            // 업데이트&렌더 쓰레드
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                // 1ms == 10,000 Ticks
                long lastTick = DateTime.Now.Ticks;
                uint currentSize = 1;
                Graphics backGraphic;
                Image backImage = new Bitmap(1, 1);
                backGraphic = Graphics.FromImage(backImage);
                var backgroundBrush = new System.Drawing.SolidBrush(Color.Blue);
                while (true)
                {
                    
                    var tick = DateTime.Now.Ticks;
                    if (tick - lastTick < 300000)
                    {
                        System.Threading.Thread.Sleep(1);
                        continue;
                    }
                    GlobalClock.Tick(tick - lastTick);
                    uint actualSize = size;
                    if (actualSize == 0)
                    {
                        break;
                    }

                    if (currentSize != actualSize)
                    {
                        backGraphic.Dispose();
                        backImage.Dispose();
                        backImage = new Bitmap((int)(actualSize >> 16), (int)(actualSize & 0x0000FFFF));
                        backGraphic = Graphics.FromImage(intersectBackImage);
                        currentSize = actualSize;
                    }

                    int width = (int)(currentSize >> 16);
                    int height = (int)(currentSize & 0x0000FFFF);

                    lastTick = tick;
                    backGraphic.FillRectangle(backgroundBrush, 0, 0, width, height);
                    World.RenderManager.Render(backGraphic);
                    lock (intersectGraphic)
                    {
                        intersectGraphic.DrawImage(backImage, 0, 0);
                    }
                    BeginInvoke(new Action(Refresh));
                }

                backGraphic.Dispose();
                backImage.Dispose();

            })).Start();

        }

        protected override void OnHandleCreated(EventArgs e)
        {

                        
            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            intersectGraphic.Dispose();
            intersectBackImage.Dispose();
            size = 0;
            base.OnHandleDestroyed(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (intersectGraphic == null)
            {
                intersectBackImage = new Bitmap(Width, Height);
                intersectGraphic = Graphics.FromImage(intersectBackImage);
            }

            lock (intersectGraphic)
            {
                intersectGraphic.Dispose();
                intersectBackImage.Dispose();
                intersectBackImage = new Bitmap(Width, Height);
                intersectGraphic = Graphics.FromImage(intersectBackImage);
            }

            size = (uint)Width << 16 | (uint)Height;
            base.OnSizeChanged(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            lock (intersectGraphic)
            {
                pe.Graphics.DrawImage(intersectBackImage, 0, 0);
            }

//            World.RenderManager.Render(pe.Graphics);
            //base.OnPaint(pe);
            //GroundInstance.Render(pe.Graphics);
        }


    }
}
