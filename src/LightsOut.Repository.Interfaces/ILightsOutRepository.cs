using LightsOut.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightsOut.Repository.Interfaces
{
    public interface ILightsOutRepository
    {
        Task<LightsOutModel> GetGameStateAsync(Guid id, CancellationToken cancellationToken = default);
        Task<LightsOutModel> InsertGameStateAsync(LightsOutModel model, CancellationToken cancellationToken = default);
        Task<LightsOutModel> SaveGameStateAsync(LightsOutModel model, CancellationToken cancellationToken = default);
    }
}
