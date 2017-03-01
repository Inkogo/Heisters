using OpenTK;

namespace Heisters
{
    class Point
    {
        public int X;
        public int Y;

        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        public static Point operator +(Point p0, Point p1)
        {
            return new Point(p0.X + p1.X, p0.Y + p1.Y);
        }

        public static Point operator -(Point p0, Point p1)
        {
            return new Point(p0.X - p1.X, p0.Y - p1.Y);
        }

        public override bool Equals(object obj)
        {
            Point p = (Point)obj;
            if (p == null) return false;
            return this.X == p.X && this.Y == p.Y;
        }

        //Jon Skeet magic!
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            return hash;
        }
    }
}
