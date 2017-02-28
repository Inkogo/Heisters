using System.Collections.Generic;

namespace Heisters
{
    class Map
    {
        public List<GameObject> transforms;

        public string name;
        public Tile[,] tiles;
        public TileType[] tileTypes;
        public Point mapSize;

        public Map(string name, int width, int height)
        {
            this.name = name;
            mapSize = new Point(width, height);
            tiles = new Tile[width, height];
            foreach (Point v in GetTiles())
            {
                tiles[v.X, v.Y] = new Tile(0);
            }
            tileTypes = new TileType[]
            {
                new TileType() { name="Wall", solid=true, renderer=new SpriteRenderer("test.png") }
            };
            transforms = new List<GameObject>();
        }

        public void OnRender()
        {
            foreach (Point v in GetTiles())
            {
                TileType t = tileTypes[tiles[v.X, v.Y].tileId];
                t.DrawTile(v);
            }
            foreach (GameObject o in transforms)
            {
                o.Draw();
            }
        }

        public void Tick()
        {
            foreach (Point p in GetTiles())
            {
                Tile t = tiles[p.X, p.Y];
                t.Tick(tileTypes[t.tileId]);
            }
        }
        
        public IEnumerable<Point> GetTiles()
        {
            for (int x = 0; x < mapSize.X; x++)
            {
                for (int y = 0; y < mapSize.Y; y++)
                {
                    yield return new Point(x, y);
                }
            }
        }
    }
}
