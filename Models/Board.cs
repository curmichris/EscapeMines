using System;
using System.Collections.Generic;
using Common.Enums;

namespace Models
{
    public class Board
    {
        public Board(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Cell[,] Cells { get; set; }
        public void SetupBoard(List<PointModel> mines, PointModel exitPoint)
        {
            Cells = new Cell[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cells[x, y] = new Cell();
                }
            }

            foreach (var Mine in mines)
            {
                Cells[Mine.XPosition, Mine.YPosition].IsMine = true;
            }

            Cells[exitPoint.XPosition, exitPoint.YPosition].IsExit = true;
        }


        public Status MakeMove(Board board, Turtle turtle)
        {
            if (IsMoveWithinBounds(turtle))
            {
                return Status.OutOfBounds;
            }
            turtle.Move();
            var cell = board.GetCurrentCell(turtle);
            if (cell.IsMine)
            {
                return Status.MineHit;
            }
            if(cell.IsExit)
            {
                return Status.GameOver;
            }

            return Status.Ok;
        }

        

        private bool IsMoveWithinBounds(Turtle turtle)
        {
            var posX = turtle.XPosition;
            var posY = turtle.YPosition;

            switch (turtle.Direction)
            {
                case Direction.North:
                    posY--;
                    break;
                case Direction.East:
                    posX++;
                    break;
                case Direction.South:
                    posY++;
                    break;
                case Direction.West:
                    posX--;
                    break;
                case Direction.Undefined:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return (posX < 0 || posX + 1 > Width || posY < 0 || posY + 1 > Height);
        }

        private Cell GetCurrentCell(Turtle turtle)
        {
            return this.Cells[turtle.XPosition, turtle.YPosition];
        }
    }
}
