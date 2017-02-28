using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using OpenTK;

namespace Heisters
{
    class Input
    {
        List<Key> pressedKeys;
        List<Key> lastPressedKeys;

        public Input(GameWindow game)
        {
            pressedKeys = new List<Key>();
            lastPressedKeys = new List<Key>();
            InjectionContainer.Instance.RegisterObject(this);

            game.KeyDown += KeyDown;
            game.KeyUp += KeyUp;
        }

        public void Update()
        {
            lastPressedKeys = new List<Key>(pressedKeys);
        }

        public bool KeyDown(Key key)
        {
            return pressedKeys.Contains(key) && !lastPressedKeys.Contains(key);
        }

        public bool KeyHold(Key key)
        {
            return pressedKeys.Contains(key) || lastPressedKeys.Contains(key);
        }

        public bool KeyUp(Key key)
        {
            return !pressedKeys.Contains(key) && lastPressedKeys.Contains(key);
        }

        void KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (!pressedKeys.Contains(e.Key))
                pressedKeys.Add(e.Key);
        }

        void KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            if (pressedKeys.Contains(e.Key))
                pressedKeys.Remove(e.Key);
        }
    }
}
