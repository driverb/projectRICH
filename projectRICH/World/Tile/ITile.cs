using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.World.Tile
{
    interface ITile
    {
        void OnEnter(Object.GameObject gameObject);
        void OnLeave(Object.GameObject gameObject);
    }
}
