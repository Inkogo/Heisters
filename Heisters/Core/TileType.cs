using OpenTK;
using OpenTK.Graphics;

namespace Heisters
{
    class TileType
    {
        public string name;
        public bool solid;
        public int durability;
        public int emission;

        public SpriteRenderer renderer;

        public void DrawTile(Point p, Tile t)
        {
            byte l = (byte)((t.lightLvl / 15f).Remap(0, 1, 50, 255));
            Color4 col = new Color4(l, l, l, 255);
            renderer.DrawSprite(new Vector2(p.X * renderer.tex.width, p.Y * renderer.tex.height), col);
        }

        virtual public void OnInit(Tile t)
        {
            t.blocked = solid;
        }

        virtual public void Tick(Tile t) { }

        virtual public void OnInteract(Entity e, Tile t) { }
    }
}
