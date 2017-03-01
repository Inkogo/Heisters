using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heisters
{
    static class MathUtils
    {
        public static float Remap(this float v, float from1, float to1, float from2, float to2)
        {
            return (v - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}
