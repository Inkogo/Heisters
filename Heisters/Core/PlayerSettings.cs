using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace Heisters
{
    class PlayerSettings
    {
        public Dictionary<PlayerAction, Key> actions;

        public PlayerSettings()
        {
            InjectionContainer.Instance.RegisterObject(this);
            actions = new Dictionary<PlayerAction, Key>()
            {
                { PlayerAction.moveLeft, Key.A },
                { PlayerAction.moveRight, Key.D },
                { PlayerAction.moveUp, Key.W },
                { PlayerAction.moveDown, Key.S },
                { PlayerAction.interact, Key.E },
                { PlayerAction.wait, Key.Q },
            };
        }
    }
}
