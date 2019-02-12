using System;
using System.Security.Cryptography.X509Certificates;
using Common.Enums;
using Interfaces;
using Models;
using Services.IO;

namespace Services
{
    public class Engine : IEngine
    {
        private readonly IReader _reader;
        private GameModel _gameModel;
        private Turtle _turtle;
        private Board _board;

        public Engine()
        {
            _reader = new Reader();
        }

        public Engine(IReader reader)
        {
            _reader = reader;
        }

        public GameSettingsModel GetGameSettings(string sequence)
        {
            _gameModel = this._reader.GetGameModel(sequence);
            try
            {
                _board = new Board(_gameModel.Size.XPosition, _gameModel.Size.YPosition);
                _board.SetupBoard(_gameModel.Mines, _gameModel.Exit);
                _gameModel.Board = _board;

                _turtle = new Turtle()
                {
                    XPosition = _gameModel.Start.XPosition,
                    YPosition = _gameModel.Start.YPosition,
                    Direction = _gameModel.Start.Direction
                };

                return new GameSettingsModel()
                {
                    GameModel = _gameModel,
                    Turtle = _turtle,
                };
            }
            catch
            {
                return new GameSettingsModel();
            }
        }
    }
}
