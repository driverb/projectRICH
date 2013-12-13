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

        public WorldView()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            GroundInstance.Render(pe.Graphics);
        }
    }
}
