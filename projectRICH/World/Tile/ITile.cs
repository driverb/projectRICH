using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.World.Tile
{
    interface ITile
    {
        void OnEnter(Object.IGameObject gameObject);
        void OnLeave(Object.IGameObject gameObject);
    }
}
