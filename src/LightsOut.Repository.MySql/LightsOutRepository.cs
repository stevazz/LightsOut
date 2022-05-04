using LightsOut.Models;
using LightsOut.Repository.Interfaces;
using LightsOut.Repository.MySql.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using LightsOut.Repository.MySql.Models;
using LightsOut.Repository.MySql.Mapper;

namespace LightsOut.Repository.MySql
{
    public class LightsOutRepository : ILightsOutRepository
    {
        private readonly ILogger<LightsOutRepository> logger;
        private readonly LightsOutMySqlConfiguration configuration = new LightsOutMySqlConfiguration();

        public LightsOutRepository(ILogger<LightsOutRepository> logger,
            LightsOutMySqlConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task<LightsOutModel> GetGameStateAsync(Guid id, CancellationToken cancellationToken = default)
        {
            using (var connection = new MySqlConnection(configuration.ConnectionString))
            {
                connection.Open();
                var model = await connection.QuerySingleOrDefaultAsync<LightsOutRepositoryModel>("SELECT * FROM state WHERE Id=@id", new { Id = id });

                return model.ToDomainModel();
            }
        }

        public async Task<LightsOutModel> InsertGameStateAsync(LightsOutModel model, CancellationToken cancellationToken = default)
        {
            using (var connection = new MySqlConnection(configuration.ConnectionString))
            {
                connection.Open();

                var repositoryModel = model.ToRepositoryModel();

                await connection.ExecuteAsync("INSERT INTO state VALUES (@Id, @Board, @ColumnLength, @RowLength, @Score);", repositoryModel);
                return model;
            }
        }

        public async Task<LightsOutModel> SaveGameStateAsync(LightsOutModel model, CancellationToken cancellationToken = default)
        {
            using (var connection = new MySqlConnection(configuration.ConnectionString))
            {
                connection.Open();

                var repositoryModel = model.ToRepositoryModel();

                await connection.ExecuteAsync("UPDATE state SET Board=@Board  WHERE Id=@Id", repositoryModel);
                return model;
            }
        }
    }
}
