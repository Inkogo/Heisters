using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heisters
{
    abstract class Tween<T>
    {
        public T start;
        public T end;
        float speed;
        float tme;

        public bool finished { get; private set; }

        public Tween(T s, T e, float t)
        {
            start = s;
            end = e;
            speed = t;
            tme = 0f;
        }

        public T Move(float delta)
        {
            tme += delta / speed;
            finished = tme >= 1f;
            tme = LerpUtils.Clamp01(tme);
            return Interpolate(tme);
        }

        abstract protected T Interpolate(float time);
    }
}
