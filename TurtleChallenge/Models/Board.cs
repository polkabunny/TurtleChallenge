using TurtleChallenge.Models.Area;

namespace TurtleChallenge.Models
{
    public class Board
    {
        public Location ExitLocation { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public Location[] MineLocations { get; set; }

        public Board(Location exitLocation, int width, int length, Location[] mineLocations)
        {
            ExitLocation = exitLocation;
            Width = width;
            Length = length;
            MineLocations = mineLocations;
        }

        public Board() {}
    }
}
