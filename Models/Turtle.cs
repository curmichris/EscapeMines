using System;
using Common.Enums;

namespace Models
{
    public class Turtle : PointModel
    {
        public Status Rotate()
        {
            switch (Direction)
            {
                case Direction.North:
                    Direction = Direction.East;
                    break;
                case Direction.East:
                    Direction = Direction.South;
                    break;
                case Direction.South:
                    Direction = Direction.West;
                    break;
                case Direction.West:
                    Direction = Direction.North;
                    break;
            }

            return Status.Ok;
        }

        public void Move()
        {
            switch (Direction)
            {
                case Direction.North:
                    YPosition--;
                    break;
                case Direction.East:
                    XPosition++;
                    break;
                case Direction.South:
                    YPosition++;
                    break;
                case Direction.West:
                    XPosition--;
                    break;
                case Direction.Undefined:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
