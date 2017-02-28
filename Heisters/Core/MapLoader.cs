using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heisters
{
    class MapLoader
    {
        public Map currentMap { get; private set; }

        public MapLoader()
        {
            InjectionContainer.Instance.RegisterObject(this);
            currentMap = new Map("asd", 16, 16);
        }
    }
}
