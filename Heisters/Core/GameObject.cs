using OpenTK.Graphics;

namespace Heisters
{
    abstract class GameObject : Transform
    {
        [Inject]
        MapLoader mapLoader;

        public SpriteRenderer renderer;
        public int depth;
        public Point tilePos;

        public GameObject() : base()
        {
            InjectionContainer.Instance.InjectDependencies(this);
            depth = 0;
            tilePos = new Point(0, 0);
        }

        virtual public void Tick() { }

        public void Draw()
        {
            renderer.DrawSprite(position, GetLightColor(), depth);
        }

        Color4 GetLightColor()
        {
            int light = mapLoader.currentMap.GetTile(tilePos.X, tilePos.Y).lightLvl;
            return new Color4(255, 255, 255, 255);
        }
    }
}
