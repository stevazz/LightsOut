using LightsOut.Engine.Interfaces;
using LightsOut.Models;
using LightsOut.Random.Interfaces;
using LightsOut.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightsOut.Engine
{
    public class LightsOutEngine : ILightsOutEngine
    {
        private readonly ILightsOutRepository repository;
        private readonly ILogger<LightsOutEngine> logger;
        private readonly IRandomProvider randomProvider;

        public LightsOutEngine(ILogger<LightsOutEngine> logger,
            ILightsOutRepository repository,
            IRandomProvider randomProvider)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.randomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
        }

        public async Task<LightsOutModel> GetAsync(Guid id)
        {
            logger.LogInformation($"Loading Game State {id}");
            var game = await repository.GetGameStateAsync(id);
            return game;
        }

        public async Task<LightsOutModel> IntializeAsync(int rowLength, int columnLength, int activeCells)
        {
            if(activeCells > rowLength * columnLength)
            {
                throw new ArgumentException("active cells cannot be larger then game size");
            }

            var game = new LightsOutModel
            {
                Id = Guid.NewGuid(),
                Score = 0,
                Board = new bool[rowLength, columnLength]
            };

            logger.LogInformation($"Intializing Game {game.Id}");

            var shuffledCells = new Queue<int>(Enumerable.Range(0, rowLength * columnLength).OrderBy(c => randomProvider.Next()));

            for (int c = 0; c < activeCells; c++)
            {
                var cell = shuffledCells.Dequeue();

                var i = cell % game.Board.GetLength(0);
                var j = cell / game.Board.GetLength(1);
                game.Board[i, j] = !game.Board[i, j];
            }

            logger.LogInformation($"Intialized Game {game.Id}");

            return await repository.InsertGameStateAsync(game);
        }

        public async Task<LightsOutModel> ToggleAsync(Guid id, int i, int j)
        {
            logger.LogInformation($"Loading Game State {id}");
            var game = await repository.GetGameStateAsync(id);

            if(i > game.Board.GetLength(0) || j > game.Board.GetLength(1))
            {
                throw new ArgumentException("Cell is outside of board!");
            }

            if (!game.IsSolved)
            {
                game.Score++;

                game.Board[i, j] = !game.Board[i, j];

                if (i < game.Board.GetLength(0) - 1)
                    game.Board[(i + 1) % game.Board.GetLength(0), j] = !game.Board[(i + 1) % game.Board.GetLength(0), j];
                if (i > 0)
                    game.Board[(i - 1) % game.Board.GetLength(0), j] = !game.Board[(i - 1) % game.Board.GetLength(0), j];
                if (j < game.Board.GetLength(1) - 1)
                    game.Board[i, (j + 1) % game.Board.GetLength(1)] = !game.Board[i, (j + 1) % game.Board.GetLength(1)];
                if (j > 0)
                    game.Board[i, (j - 1) % game.Board.GetLength(1)] = !game.Board[i, (j - 1) % game.Board.GetLength(1)];

                logger.LogInformation($"Saving Game State {id}");
                return await repository.SaveGameStateAsync(game);
            }

            throw new ApplicationException();
        }
    }
}
