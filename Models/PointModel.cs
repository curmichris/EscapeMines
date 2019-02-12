using Common.Enums;

namespace Models
{
    public class PointModel
    {
        public PointModel(){ }

        public PointModel(int xPosition, int yPosition, Direction direction = Direction.Undefined)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Direction = direction;
        }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public Direction Direction { get; set; }
    }
}
