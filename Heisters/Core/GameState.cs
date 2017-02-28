using System;

namespace Heisters
{
    class GameState
    {
        public MapLoader mapLoader;
        public Player player;
        public DateTime lastPlayed;
        public DateTime createdAt;

        public static GameState LoadOrCreate()
        {
            return new GameState();
        }

        public GameState()
        {
            mapLoader = new MapLoader();
            player = new Player() { renderer = new SpriteRenderer("test2.png"), depth = 1, };
            mapLoader.currentMap.transforms.Add(player);
            lastPlayed = createdAt = DateTime.Now;
        }

        public void Update(float delta)
        {
            if (player.Update(delta))
            {
                mapLoader.currentMap.Tick();
            }
        }
    }
}
