using System;
using System.IO;
using System.Linq;
using TurtleChallenge.Files;
using TurtleChallenge.Models;

namespace TurtleChallenge
{
    public class Program
    {
        private static Turtle _turtle;
        private static Board _board;

        public static void Main(string[] args)
        {
            try
            {
                var settingsFile = args[0];
                var movesFile = args[1];

                _turtle = new Turtle {Moves = InputFile.ReadMovesFile(movesFile)};
                _board = new Board();
                InputFile.ReadSettingsFile(settingsFile, _turtle, _board);

                _turtle.MakeAllMoves(_board);
            }
            catch (IOException e)
            {
                Console.Write($"Error with file: {e.Message}{Environment.NewLine}Stack trace: {e.StackTrace}");
            }
            catch (NullReferenceException e)
            {
                Console.Write($"Oops! Something went wrong: {e.Message}{Environment.NewLine}Stack trace: {e.StackTrace}");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Write($"We went out of bounds: {e.Message}{Environment.NewLine}Stack trace: {e.StackTrace}");
            }
        }
    }
}
