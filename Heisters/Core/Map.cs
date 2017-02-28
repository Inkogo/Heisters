using System.Collections.Generic;
using System;

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

            Random rnd = new Random();
            foreach (Point v in GetTiles())
            {
                int r = rnd.Next(0, 2);
                tiles[v.X, v.Y] = new Tile(r);
            }
            tileTypes = new TileType[]
            {
                new TileType() { name="Wall", solid=true, renderer=new SpriteRenderer("test.png") },
                new TileType() { name="Floor", solid=false, renderer=new SpriteRenderer("test3.png") },
            };
            foreach(Point p in GetTiles())
            {
                Tile t = GetTile(p.X, p.Y);
                GetTileType(t).OnInit(t);
            }
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
                Tile t = GetTile(p.X, p.Y);
                GetTileType(t).Tick(t);
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

        public Tile GetTile(int x, int y)
        {
            if (!CheckTileInBounds(x, y)) return null;
            return tiles[x, y];
        }

        public bool CheckTileInBounds(int x, int y)
        {
            return x >= 0 && x < mapSize.X && y >= 0 && y < mapSize.Y;
        }

        public TileType GetTileType(int x, int y)
        {
            Tile t = GetTile(x, y);
            if (t == null) return null;
            return GetTileType(t);
        }

        public TileType GetTileType(Tile t)
        {
            return tileTypes[t.tileId];
        }

        public List<Tile> GetTilesInRange(int r, Point p, Func<Tile, bool> onCheckTile, int depth = 0)
        {
            List<Tile> list = new List<Tile>();
            Tile t = GetTile(p.X, p.Y);
            if (onCheckTile(t)) list.Add(t);

            if (depth < r)
            {
                foreach (Point a in GetTileNeighbors(p.X, p.Y))
                {
                    list.AddRange(GetTilesInRange(r, a, onCheckTile, depth + 1));
                }
            }
            return list;
        }

        public IEnumerable<Point> GetTileNeighbors(int x, int y)
        {
            List<Point> points = new List<Point>()
            {
                new Point(x-1,y),
                new Point(x+1,y),
                new Point(x,y-1),
                new Point(x,y+1),
            };
            foreach (Point p in points)
            {
                Tile t = GetTile(p.X, p.Y);
                if (t != null)
                    yield return p;
            }
        }
    }
}