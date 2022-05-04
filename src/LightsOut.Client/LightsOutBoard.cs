using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut.Client
{
    public class LightsOutBoard
    {
        private readonly bool[,] boardState;

        public LightsOutBoard(bool[,] boardState)
        {
            this.boardState = boardState;
        }

        public void Draw()
        {
            StringBuilder board = new StringBuilder();

            for (int i = 0; i < boardState.GetLength(0); i++)
            {
                board.Append((char)(i + 65));
                for (int j = 0; j < boardState.GetLength(1); j++)
                {
                    board.Append(boardState[i, j] ? "X|" : " |");
                }
                board.AppendLine();
            }

            board.Append(' ');
            for (int i = 0; i < boardState.GetLength(1); i++)
            {
                board.Append($"{(i + 1)} ");
            }
            board.AppendLine();

            Console.WriteLine(board.ToString());
        }

    }
}
