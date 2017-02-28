using System;
using OpenTK;
using System.Linq;

namespace Heisters
{
    class Player : Entity
    {
        class TweenPosition : Tween<Vector2>
        {
            public TweenPosition(Vector2 s, Vector2 e, float t) : base(s, e, t) { }
            protected override Vector2 Interpolate(float t)
            {
                return new Vector2(LerpUtils.Lerp(start.X, end.X, t), LerpUtils.Lerp(start.Y, end.Y, t));
            }
        }

        [Inject]
        Input input;

        [Inject]
        PlayerSettings settings;

        [Inject]
        MapLoader mapLoader;

        public string name;
        public int maxHp;
        public int hp;
        public Point tilePos;

        TweenPosition moveTween;

        public Player() : base()
        {
            InjectionContainer.Instance.InjectDependencies(this);
            moveTween = null;
            tilePos = new Point(0, 0);
        }

        public bool Update(float deltaTime)
        {
            if (moveTween != null)
            {
                position = moveTween.Move(deltaTime);
                if (moveTween.finished) moveTween = null;
                return false;
            }

            foreach (PlayerAction a in settings.actions.Keys)
            {
                if (input.KeyHold(settings.actions[a]))
                {
                    switch (a)
                    {
                        case PlayerAction.moveLeft:
                            return Move(-1, 0);
                        case PlayerAction.moveRight:
                            return Move(+1, 0);
                        case PlayerAction.moveDown:
                            return Move(0, -1);
                        case PlayerAction.moveUp:
                            return Move(0, +1);
                        case PlayerAction.interact:
                            return Interact();
                        case PlayerAction.wait:
                            return true;
                    }
                }
            }
            return false;
        }

        bool Move(int x, int y)
        {
            Point p =new Point( tilePos.X + x, tilePos.Y+y);
            Tile t = mapLoader.currentMap.GetTile(p.X, p.Y);
            if (t != null && !t.blocked)
            {
                tilePos = p;
                Vector2 newPos = new Vector2(tilePos.X * 32f, tilePos.Y * 32f);
                moveTween = new TweenPosition(position, newPos, .2f);
                return true;
            }
            return false;
        }

        bool Interact()
        {
            Tile t = mapLoader.currentMap.GetTilesInRange(1, tilePos, (Tile a) => a.interactable == true).FirstOrDefault();
            if (t != null)
            {
                mapLoader.currentMap.GetTileType(t).OnInteract(this, t);
                return true;
            }
            return false;
        }

        public Matrix4 GetViewMatrix(int width, int height)
        {
            Matrix4 view = Matrix4.Identity;
            view = Matrix4.Mult(view, Matrix4.CreateTranslation(-position.X, -position.Y, 0));
            view = Matrix4.Mult(view, Matrix4.CreateRotationZ(0));
            view = Matrix4.Mult(view, Matrix4.CreateScale(1f / width * 2f, 1f / height * 2f, 1f));

            return view;
        }
    }
}
