using System;
using System.Collections.Generic;
using System.IO;
using Common.Enums;
using Interfaces;
using Models;

namespace Services.IO
{
    public class Reader : IReader
    {
        private static string FilesPath = @"..\\..\\..\\Services\\IO\\Files\\";
        public Reader()
        {
        }

        public List<string> GetGameSequences()
        {
            var list = new List<string>();
            using (var sr = new StreamReader(FilesPath + "GameSequenceNames.csv"))
            {
                while (!sr.EndOfStream)
                {
                    var val = sr.ReadLine();
                    if (!string.IsNullOrEmpty(val))
                    {
                        list.Add(val);
                    }
                }

                return list;
            }
        }

        public GameModel GetGameModel(string sequence)
        {
            var settingString = File.ReadAllLines(FilesPath + sequence + ".csv");
            var settings = new GameModel
            {
                Size = ParseGameFile(settingString, GameIndexes.Board),
                Start = ParseGameFile(settingString, GameIndexes.StartPosition),
                Exit = ParseGameFile(settingString, GameIndexes.ExitPosition),
                Mines = new List<PointModel>()
                    {
                        ParseGameFile(settingString, GameIndexes.Mine1),
                        ParseGameFile(settingString, GameIndexes.Mine2),
                        ParseGameFile(settingString, GameIndexes.Mine3)
                    },
                Moves = settingString[Convert.ToInt32(GameIndexes.Moves)].Split(',')
            };

            return ValidateFile(settings) ? settings : new GameModel();
        }

        private bool ValidateFile(GameModel settings)
        {
            if (settings.Size.XPosition < 0 || settings.Size.YPosition < 0)
            {
                return false;
            }

            if (settings.Start.XPosition < 0 || settings.Start.YPosition < 0 ||
                settings.Start.XPosition > settings.Size.XPosition || settings.Start.YPosition > settings.Size.YPosition)
            {
                return false;
            }

            if (settings.Exit.XPosition < 0 || settings.Exit.YPosition < 0 ||
                settings.Exit.XPosition > settings.Size.XPosition || settings.Exit.YPosition > settings.Size.YPosition)
            {
                return false;
            }

            if (settings.Exit.XPosition < 0 || settings.Exit.YPosition < 0 ||
                settings.Exit.XPosition > settings.Size.XPosition || settings.Exit.YPosition > settings.Size.YPosition)
            {
                return false;
            }

            if (settings.Moves.Length <= 0)
            {
                return false;
            }

            return true;

        }

        private PointModel ParseGameFile(string[] file, GameIndexes index)
        {
            var values = file[Convert.ToInt32(index)].Split(',');

            return new PointModel(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), GetDirection(values[2]));
        }

        private Direction GetDirection(string dir)
        {
            if (!string.IsNullOrEmpty(dir))
            {
                switch (dir)
                {
                    case "N":
                        return Direction.North;
                    case "S":
                        return Direction.South;
                    case "E":
                        return Direction.East;
                    case "W":
                        return Direction.West;
                    default: return Direction.Undefined;
                }
            }

            return Direction.Undefined;
        }
    }
}

