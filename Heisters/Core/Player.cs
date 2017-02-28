using System;
using OpenTK;

namespace Heisters
{
    class Player : GameObject
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

        public string name;
        public int maxHp;
        public int hp;

        TweenPosition moveTween;

        public Player() : base()
        {
            InjectionContainer.Instance.InjectDependencies(this);
            moveTween = null;
        }

        // maybe move this in gameState so I have map object for checkCasting
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
                            Move(-1, 0);
                            return true;
                        case PlayerAction.moveRight:
                            Move(+1, 0);
                            return true;
                        case PlayerAction.moveDown:
                            Move(0, -1);
                            return true;
                        case PlayerAction.moveUp:
                            Move(0, +1);
                            return true;
                        case PlayerAction.interact:
                            return true;
                        case PlayerAction.wait:
                            return true;
                    }
                }
            }
            return false;
        }

        void Move(int x, int y)
        {
            Vector2 newPos = new Vector2(position.X + (x * 32f), position.Y + (y * 32f));
            moveTween = new TweenPosition(position, newPos, .2f);
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
