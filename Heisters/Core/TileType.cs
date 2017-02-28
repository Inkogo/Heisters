using OpenTK;

namespace Heisters
{
    class TileType
    {
        public string name;
        public bool solid;
        public int durability;

        public SpriteRenderer renderer;

        public void DrawTile(Point p)
        {
            renderer.DrawSprite(new Vector2(p.X * renderer.tex.width, p.Y * renderer.tex.height));
        }
    }
}
