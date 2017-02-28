using System;

namespace Heisters
{
    class GameState
    {
        public Map map;
        public Player player;
        public DateTime lastPlayed;
        public DateTime createdAt;

        public static GameState LoadOrCreate()
        {
            return new GameState();
        }

        public GameState()
        {
            player = new Player() { renderer = new SpriteRenderer("test2.png"), depth = 1, };
            map = new Map("asd", 16, 16);
            map.transforms.Add(player);
            lastPlayed = createdAt = DateTime.Now;
        }

        public void Update(float delta)
        {
            if (player.Update(delta))
            {
                map.Tick();
            }
        }
    }
}
