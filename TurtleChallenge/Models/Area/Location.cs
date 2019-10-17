namespace TurtleChallenge.Models.Area
{
    public class Location
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }

        public Location(int x, int y)
        {
            XCoord = x;
            YCoord = y;
        }

        public Location(string x, string y)
        {
            XCoord = int.Parse(x);
            YCoord = int.Parse(y);
        }

        public bool LocationOverlaps(Location first, Location second)
        {
            return (first.XCoord == second.XCoord) && (first.YCoord == second.YCoord);
        }

        public static Location[] GetLocations(string[] locations)
        {
            var returnLocations = new Location[locations.Length];
            for (int i = 0; i < locations.Length; i++)
            {
                var split = locations[i].Split(',');
                returnLocations[i] = new Location(split[0], split[1]);
            }

            return returnLocations;
        }
    }
}
