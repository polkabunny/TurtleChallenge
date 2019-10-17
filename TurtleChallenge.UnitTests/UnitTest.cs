using NUnit.Framework;
using TurtleChallenge.Models;
using TurtleChallenge.Models.Area;
using TurtleChallenge.Files;

namespace TurtleChallenge.UnitTests
{
    public class Tests
    {
        private Turtle _turtle;
        private Location _location;
        private Orientation _orientation;
        private Board _board;
        private Location _exitLocation;
        private Location[] _mines;

            [SetUp]
        public void Setup()
        {
            _turtle = new Turtle();
            _location = new Location(5, 10);
            _orientation = new Orientation();

            _exitLocation = new Location(5,5);
            _mines = new[] { new Location(6, 10) };
            _board = new Board(_exitLocation, 10, 10, _mines);
        }

        [Test]
        public void LocationHasCorrectVariables()
        {
            Assert.AreEqual(_location.XCoord, 5);
            Assert.AreEqual(_location.YCoord, 10);
        }

        [Test]
        public void TurtleCanTurn()
        {
            _turtle = new Turtle(_orientation, _location);
            _turtle.Rotate();
            Assert.AreNotEqual(_orientation.direction, _turtle.Orientation.direction);
        }

        [Test]
        public void TurtleCanMove()
        {
            _turtle = new Turtle(_orientation, new Location(5, 10));
            _turtle.MakeMove('m',_board);
            Assert.AreNotEqual(_location.XCoord, _turtle.Location.XCoord);
            Assert.AreEqual(_location.YCoord, _turtle.Location.YCoord);
        }

        [Test]
        public void TurtleWillExplode()
        {
            _turtle = new Turtle(_orientation, _location);
            _turtle.MakeMove('m',_board);
            Assert.AreEqual(_board.MineLocations[0].XCoord, _turtle.Location.XCoord);
            Assert.AreEqual(_board.MineLocations[0].YCoord, _turtle.Location.YCoord);
            Assert.IsTrue(_turtle.CurrentStatus == Turtle.Status.MineHit);
        }

        [Test]
        public void TurtleWillSucceed()
        {
            _turtle = new Turtle(new Orientation(), new Location(4, 5));
            _turtle.MakeMove('m', _board);
            Assert.AreEqual(_board.ExitLocation.XCoord, _turtle.Location.XCoord);
            Assert.AreEqual(_board.ExitLocation.YCoord, _turtle.Location.YCoord);
            Assert.AreEqual(_turtle.CurrentStatus, Turtle.Status.Success);
        }

        [Test]
        public void TurtleInDanger()
        {
            _turtle = new Turtle(new Orientation(), new Location(1,1));
            Assert.AreNotEqual(_exitLocation.XCoord, _turtle.Location.XCoord);
            Assert.AreNotEqual(_exitLocation.YCoord, _turtle.Location.YCoord);
            Assert.AreEqual(_turtle.CurrentStatus, Turtle.Status.StillInDanger);
        }

        [Test]
        public void CorrectSettingsConfiguredFromFile()
        {
            var localTurtle = new Turtle();
            var localBoard = new Board();
            
            var testFile = "TestFiles\\game-settings.txt";
            InputFile.ReadSettingsFile(testFile, localTurtle, localBoard);
            Assert.IsNull(localTurtle.Moves);
            Assert.IsTrue(localTurtle.CurrentStatus == Turtle.Status.StillInDanger);
            Assert.IsTrue(localTurtle.Orientation.direction == Orientation.Direction.North);
            Assert.IsTrue(localBoard.MineLocations.Length > 0);
            Assert.IsNotNull(localBoard.ExitLocation);
            Assert.IsTrue(localBoard.Length > 0);
            Assert.IsTrue(localBoard.Width > 0);
        }

        [Test]
        public void MovesConfiguredFromFile()
        {
            var testFile = "TestFiles\\moves.txt";
            var localTurtle = new Turtle {Moves = InputFile.ReadMovesFile(testFile)};
            Assert.IsTrue(localTurtle.Moves.Count > 0);
        }

        [Test]
        public void TurtleMakesFileMoves_Success()
        {
            var testMovesFile = "TestFiles\\successmoves.txt";
            var localTurtle = new Turtle { Moves = InputFile.ReadMovesFile(testMovesFile) };
            var localBoard = new Board();

            var testSettingsFile = "TestFiles\\game-settings.txt";
            InputFile.ReadSettingsFile(testSettingsFile, localTurtle, localBoard);

            localTurtle.MakeAllMoves(localBoard);

            Assert.IsTrue(localTurtle.CurrentStatus == Turtle.Status.Success);
        }

        [Test]
        public void TurtleMakesFileMoves_MineHit()
        {
            var testMovesFile = "TestFiles\\minehit-moves.txt";
            var localTurtle = new Turtle { Moves = InputFile.ReadMovesFile(testMovesFile) };
            var localBoard = new Board();

            var testSettingsFile = "TestFiles\\game-settings.txt";
            InputFile.ReadSettingsFile(testSettingsFile, localTurtle, localBoard);

            localTurtle.MakeAllMoves(localBoard);

            Assert.IsTrue(localTurtle.CurrentStatus == Turtle.Status.MineHit);
        }
    }
}