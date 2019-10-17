using System;

namespace TurtleChallenge.Models.Area
{
    public class Orientation
    {
        public enum Direction
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }

        public Direction direction = Direction.North;

        public Orientation() { }

        public Orientation(Direction d)
        {
            direction = d;
        }

        public static Orientation MapOrientation(string s)
        {
            if (s.Contains("e", StringComparison.InvariantCultureIgnoreCase))
                return new Orientation(Direction.East);
            if (s.Contains("s", StringComparison.InvariantCultureIgnoreCase))
                return new Orientation(Direction.South);
            if (s.Contains("w", StringComparison.InvariantCultureIgnoreCase))
                return new Orientation(Direction.West);
            return new Orientation(Direction.North);
        }
    }
}
