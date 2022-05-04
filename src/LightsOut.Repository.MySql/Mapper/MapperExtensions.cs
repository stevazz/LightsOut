using LightsOut.Models;
using LightsOut.Repository.MySql.Models;

namespace LightsOut.Repository.MySql.Mapper
{
    public static class MapperExtensions
    {
        public static LightsOutRepositoryModel ToRepositoryModel(this LightsOutModel model)
        {
            string state = "";
            for (int i = 0; i < model.Board.GetLength(0); i++)
            {
                for (int j = 0; j < model.Board.GetLength(1); j++)
                {
                    state += model.Board[i, j] ? '1' : '0';
                }
            }

            var repositoryModel = new LightsOutRepositoryModel
            {
                Id = model.Id,
                Board = state,
                RowLength = model.Board.GetLength(0),
                ColumnLength = model.Board.GetLength(1),
                Score = model.Score
            };

            return repositoryModel;
        }

        public static LightsOutModel ToDomainModel(this LightsOutRepositoryModel repositoryModel)
        {
            bool[,] state = new bool[repositoryModel.RowLength, repositoryModel.ColumnLength];
            for (int i = 0; i < repositoryModel.RowLength; i++)
            {
                for (int j = 0; j < repositoryModel.ColumnLength; j++)
                {
                    state[i, j] = repositoryModel.Board[(i * repositoryModel.ColumnLength) + j] == '1' ? true : false;
                }
            }

            return new LightsOutModel
            {
                Id = repositoryModel.Id,
                Board = state,
                Score = repositoryModel.Score
            };
        }

    }
}
