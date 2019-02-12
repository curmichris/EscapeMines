using System;
using System.ComponentModel;
using System.Linq;
using Common.Enums;
using Models;
using Services;
using Services.IO;


namespace TurtleMines
{
    class Program
    {
        private static Status _gameStatus = Status.GameStart;
        static void Main(string[] args)
        {
            var sequences = new Reader().GetGameSequences();
            if (sequences.Count > 0)
            {
                var engine = new Engine();
                foreach (var sequence in sequences)
                {
                    Play(engine.GetGameSettings(sequence));
                }

                Console.WriteLine("Game Over");
            }
            else
            {
                Console.WriteLine("No Sequence file provided.");
            }
            
            Console.ReadKey();
        }

        private static void Play(GameSettingsModel settings)
        {
            if (settings.GameModel != null)
            {
                var moves = settings.GameModel.Moves;
                SetStartupMessage("Starting Position", settings.GameModel.Start);
                SetStartupMessage("Exit Position", settings.GameModel.Exit);
                foreach (var o in settings.GameModel.Mines.Select((mine, c) => new { c, mine }))
                {
                    SetStartupMessage($"Mine {o.c}", o.mine);
                }

                foreach (var move in moves)
                {
                    Console.WriteLine($"Next Move: {move}");

                    switch (move)
                    {
                        case "Move":
                            _gameStatus = TakeAction(Maneuver.Move, settings.GameModel.Board, settings.Turtle);
                            break;
                        case "Turn":
                            _gameStatus = TakeAction(Maneuver.Turn, settings.GameModel.Board, settings.Turtle);
                            Console.WriteLine($"Turtle now facing {settings.Turtle.Direction}");
                            break;
                    }

                    Console.WriteLine($"Turtle Now at X: {settings.Turtle.XPosition} - Y: {settings.Turtle.YPosition}, Facing {settings.Turtle.Direction}");
                    if (_gameStatus == Status.GameOver || _gameStatus == Status.MineHit || _gameStatus == Status.OutOfBounds)
                    {
                        break;
                    }
                }

                switch (_gameStatus)
                {
                    case Status.Ok:
                        Console.WriteLine("STILL IN DANGER!!!");
                        Console.WriteLine("Turtle never found the way out and got lost!");
                        Console.WriteLine("--------------");
                        break;
                    case Status.MineHit:
                        Console.WriteLine("MINE HIT!!!");
                        Console.WriteLine("Turtle has hit a Mine on his trail and Died! :(");
                        Console.WriteLine("--------------");
                        break;
                    case Status.OutOfBounds:
                        Console.WriteLine("OUT OF BOUNDS");
                        Console.WriteLine("Turtle went out of bounds");
                        Console.WriteLine("--------------");
                        break;
                    case Status.GameOver:
                        Console.WriteLine("SUCCESS");
                        Console.WriteLine("Turtle managed to exit the trail successfully! :) ");
                        Console.WriteLine("--------------");
                        break;
                }
            }
            else
            {
                Console.Write("Configuration File provided is incorrect.");
            }
        }

        private static Status TakeAction(Maneuver maneuver, Board board, Turtle turtle)
        {
            switch (maneuver)
            {
                case Maneuver.Move:
                    return board.MakeMove(board, turtle);
                case Maneuver.Turn:
                    return turtle.Rotate();
                default:
                    Console.WriteLine("Move not recognized. Quitting Game");
                    return Status.GameOver;
            }
        }

        private static void SetStartupMessage(string topic, PointModel point)
        {
            Console.WriteLine($"{topic} set at: {point.XPosition}, {point.YPosition}");
        }

    }
}
