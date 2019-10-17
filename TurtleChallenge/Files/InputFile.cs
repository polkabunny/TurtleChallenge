using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TurtleChallenge.Models;
using TurtleChallenge.Models.Area;

namespace TurtleChallenge.Files
{
    public class InputFile
    {
        public static void ReadSettingsFile(string filePath, Turtle turtle, Board board)
        {
            try
            {
                using var reader = new StreamReader(filePath);
                var boardSize = reader.ReadLine();
                var startingPoint = reader.ReadLine();
                var direction = reader.ReadLine();
                var exitPoint = reader.ReadLine();
                var mines = reader.ReadLine();

                const string regex = @"[a-zA-Z:\s+]*";
                var splitBoardSize = Regex.Replace(boardSize, regex, string.Empty).Split(';');
                var splitStartingPoint = Regex.Replace(startingPoint, regex, string.Empty).Split(',');
                var splitDirection = direction.Split(':');
                var splitExitPoint = Regex.Replace(exitPoint, regex, string.Empty).Split(',');
                var splitMines = Regex.Replace(mines, regex, string.Empty).Split(';');

                turtle.Location = new Location(splitStartingPoint[0], splitStartingPoint[1]);
                turtle.Orientation = Orientation.MapOrientation(splitDirection[1].Trim());

                board.Length = int.Parse(splitBoardSize[0]);
                board.Width = int.Parse(splitBoardSize[1]);
                board.ExitLocation = new Location(splitExitPoint[0], splitExitPoint[1]);
                board.MineLocations = Location.GetLocations(splitMines);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException($"Error in {nameof(ReadSettingsFile)}, please check your settings file!", e.InnerException);
            }
        }

        public static Dictionary<int, char[]> ReadMovesFile(string filePath)
        {
            try
            {
                var dict = new Dictionary<int, char[]>();
                using var reader = new StreamReader(filePath);
                var i = 1;
                while (reader.Peek() > 0)
                {
                    dict.Add(i,ProcessMoveSequence(reader.ReadLine()));
                    i++;
                }

                return dict;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException($"Error in {nameof(ReadMovesFile)}, please check your moves file!", e.InnerException);
            }
        }

        public static char[] ProcessMoveSequence(string fileContents)
        {
            var moves = new char[fileContents.Length];
            for(var i = 0; i < moves.Length; i++)
            {
                moves[i] = fileContents[i];
            }

            return moves;
        }
    }
}
