using LightsOut.Models;
using LightsOut.Repository.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightsOut.Repository.Redis
{
    public class LightsOutRedisRepository : ILightsOutRepository
    {
        private readonly IDistributedCache distributedCache;
        public LightsOutRedisRepository(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task<LightsOutModel> GetGameStateAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await distributedCache.GetStringAsync(id.ToString(), cancellationToken);

            if(response != null)
                return JsonConvert.DeserializeObject<LightsOutModel>(response);
            throw new ArgumentException();
        }

        public Task<LightsOutModel> InsertGameStateAsync(LightsOutModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<LightsOutModel> SaveGameStateAsync(LightsOutModel model, CancellationToken cancellationToken = default)
        {
            var value = JsonConvert.SerializeObject(model);
            await distributedCache.SetStringAsync(model.ToString(), value);
            return model;
        }
    }
}
