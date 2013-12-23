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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            World.GameObjectManagers.Player.Execute("SetPosition", new Entity.Vector(100, 100));
            World.GameObjectManagers.Player.Execute("Show");
        }

        private void worldView_Click(object sender, EventArgs e)
        {
            MouseEventArgs arg = (MouseEventArgs)e;

            World.GameObjectManagers.Player.Execute("WalkTo", new Entity.Vector(arg.X, arg.Y));
        }
    }
}
