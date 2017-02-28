using OpenTK;

namespace Heisters
{
    class Transform
    {
        public Vector2 position;
        public float rotation;

        public Transform()
        {
            position = new Vector2(0, 0);
            rotation = 0f;
        }
    }
}
