using System;
using System.Collections.Generic;
using System.Linq;
using TurtleChallenge.Models.Area;
using static TurtleChallenge.Models.Area.Orientation;

namespace TurtleChallenge.Models
{
    public class Turtle
    {
        public enum Status
        {
            StillInDanger = 0,
            Success = 1,
            MineHit = 2
        }

        public Orientation Orientation { get; set; }
        public Location Location { get; set; }
        public Status CurrentStatus { get; set; } = 0; // default in danger
        public Dictionary<int,char[]> Moves { get; set; }

        public Turtle() { }

        public Turtle(Orientation orientation, Location location)
        {
            Orientation = orientation;
            Location = location;
        }

        public void Move(Board currentBoard)
        {
            switch (Orientation.direction)
            {
                case Direction.North:
                    if (Location.YCoord <= currentBoard.Width-1 && Location.YCoord >= 0)
                        Location.YCoord++;
                    CurrentStatus = CheckLocationStatus(currentBoard);
                    break;
                case Direction.East:
                    if (Location.XCoord <= currentBoard.Length-1 && Location.XCoord >= 0)
                        Location.XCoord++;
                    CurrentStatus = CheckLocationStatus(currentBoard);
                    break;
                case Direction.South:
                    if (Location.YCoord <= currentBoard.Width-1 && Location.YCoord >= 0)
                        Location.YCoord--;
                    CurrentStatus = CheckLocationStatus(currentBoard);
                    break;
                case Direction.West:
                    if (Location.XCoord <= currentBoard.Length-1 && Location.XCoord >= 0)
                        Location.XCoord--;
                    CurrentStatus = CheckLocationStatus(currentBoard);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Rotate()
        {
            switch (Orientation.direction)
            {
                case Direction.North:
                    Orientation = new Orientation(Direction.East);
                    break;
                case Direction.East:
                    Orientation = new Orientation(Direction.South);
                    break;
                case Direction.South:
                    Orientation = new Orientation(Direction.West);
                    break;
                default:
                    Orientation = new Orientation(Direction.North); //always default back to north
                    break;
            }
        }

        public void MakeMove(char nextMove, Board currentBoard)
        {
            if(char.ToLower(nextMove).Equals('r')) //convert to char lower in an attempt to handle some of the other possible inputs
                Rotate();
            if(char.ToLower(nextMove).Equals('m'))
                Move(currentBoard);
            // if i had the time would be cool to add different formats and files support...
        }

        public Status CheckLocationStatus(Board currentBoard)
        {
            if (Location.LocationOverlaps(Location, currentBoard.ExitLocation))
                return Status.Success;
            var mineHit = false;
            foreach (var mine in currentBoard.MineLocations)
            {
                if ((mine.XCoord == Location.XCoord) && (mine.YCoord == Location.YCoord))
                    mineHit = true;
            }
            if (mineHit)
                return Status.MineHit;
            return Status.StillInDanger;
        }

        public string MapStatus()
        {
            switch (CurrentStatus)
            {
                case Status.Success:
                    return "Success!";
                case Status.MineHit:
                    return "Mine hit!";
                case Status.StillInDanger:
                    return "Still in danger!";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void MakeAllMoves(Board board)
        {
            foreach (var array in Moves.Values)
            {
                foreach (var move in array)
                {
                    if (CurrentStatus != Turtle.Status.StillInDanger)
                        break;
                    MakeMove(move, board);
                }

                var dictionaryKey = Moves.First(x => x.Value == array).Key;
                Console.WriteLine($"Sequence {dictionaryKey}: {MapStatus()}");
            }
        }
    }
}
