using System.Collections.Generic;
using Common.Enums;

namespace Models
{
    public class GameModel
    {
        public GameModel()
        {
            Mines = new List<PointModel>();
        }

        public PointModel Size { get; set; }
        public PointModel Start { get; set; }
        public PointModel Exit { get; set; }
        public List<PointModel> Mines { get; set; } 
        public Board Board { get; set; }
        public string[] Moves { get; set; }
    }
}
