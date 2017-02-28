namespace Heisters
{
    abstract class GameObject : Transform
    {
        public SpriteRenderer renderer;
        public int depth;

        public GameObject () : base()
        {
            depth = 0;
        }

        virtual public void Tick() { }

        public void Draw ()
        {
            renderer.DrawSprite(position, depth);
        }
    }
}
