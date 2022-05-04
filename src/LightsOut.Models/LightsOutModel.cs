using System;

namespace LightsOut.Models
{
    public class LightsOutModel
    {
        public Guid Id { get; set; }
        public bool[,] Board { get; set; } = new bool[0, 0];
        public int Score { get; set; }
        public bool IsSolved
        {
            get
            {
                for (int i = 0; i < Board.GetLength(0); i++)
                {
                    for (int j = 0; j < Board.GetLength(1); j++)
                    {
                        if (!Board[i, j])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
    }
}
