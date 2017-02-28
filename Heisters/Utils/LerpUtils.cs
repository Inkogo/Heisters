using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heisters
{
    class LerpUtils
    {
        public static float Lerp(float s, float e, float t)
        {
            return s + ((e - s) * t);
        }

        public static float Clamp01(float f)
        {
            if (f < 0f) return 0f;
            else if (f > 1f) return 1f;
            return f;
        }
    }
}
