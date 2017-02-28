using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heisters
{
    class Tile
    {
        public int tileId;
        public int zoneId;
        public int lightLvl;
        public bool blocked;
        public bool interactable;

        public Tile(int i)
        {
            tileId = i;
        }
    }
}
