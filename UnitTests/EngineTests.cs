using Common.Enums;
using Interfaces;
using Models;
using NSubstitute;
using NUnit.Framework;
using Services;
using System.Collections.Generic;

namespace UnitTests
{
    public class EngineTests
    {
        private IEngine sut;
        private IReader reader;


        [SetUp]
        public void Init()
        {
            reader = Substitute.For<IReader>();
            sut = new Engine(reader);
        }

        [Test]
        public void TurtleIsCreatedCorrectlyFromGameSettingsTest()
        {
            //Arrange
            var gameModel = GetGameModelForTest();
            this.reader.GetGameModel(string.Empty).Returns(gameModel);

            //Act
            var result = sut.GetGameSettings(string.Empty);

            //Assert
            Assert.IsInstanceOf<Turtle>(result.Turtle);
            Assert.AreEqual(1, result.Turtle.XPosition);
            Assert.AreEqual(3, result.Turtle.YPosition);
            Assert.AreEqual(Direction.North, result.Turtle.Direction);
        }

        [Test]
        public void BoardIsCreatedCorrectlyFromGameSettingsTest()
        {
            //Arrange
            var gameModel = GetGameModelForTest();
            this.reader.GetGameModel(string.Empty).Returns(gameModel);

            //Act
            var result = sut.GetGameSettings(string.Empty);

            //Assert
            Assert.IsInstanceOf<Board>(result.GameModel.Board);
            Assert.AreEqual(5, result.GameModel.Board.Width);
            Assert.AreEqual(5, result.GameModel.Board.Height);
        }

        [Test]
        public void ValidationSuccessFulWithIncorrectConfig()
        {
            //Arrange
            var gameModel = GetGameModelForErrorTest();
            this.reader.GetGameModel(string.Empty).Returns(gameModel);

            //Act
            var result = sut.GetGameSettings(string.Empty);

            //Assert
            Assert.IsInstanceOf<GameSettingsModel>(result);
            Assert.IsNull(result.GameModel);
        }

        private static GameModel GetGameModelForTest()
        {
            return new GameModel
            {
                Size = new PointModel(5, 5),
                Start = new PointModel(1, 3, Direction.North),
                Exit = new PointModel(1, 1),
                Mines = new List<PointModel>()
                {
                    new PointModel(1,1),
                    new PointModel(1,2),
                    new PointModel(1,3),
                },
                Moves = new string[] { "Test", "Test" }
            };
        }
        private static GameModel GetGameModelForErrorTest()
        {
            return new GameModel
            {
                Size = new PointModel(0, 0),
                Start = new PointModel(1, 3, Direction.North),
                Exit = new PointModel(1, 1),
                Mines = new List<PointModel>()
                {
                    new PointModel(1,1),
                    new PointModel(1,2),
                    new PointModel(1,3),
                },
                Moves = new string[] { "Test", "Test" }
            };
        }
    }
}
