using LightsOut.Models;
using System;
using System.Threading.Tasks;

namespace LightsOut.Engine.Interfaces
{
    public interface ILightsOutEngine
    {
        public Task<LightsOutModel> IntializeAsync(int rowLength, int columnLength, int activeCells);
        Task<LightsOutModel> ToggleAsync(Guid id, int i, int j);
        Task<LightsOutModel> GetAsync(Guid id);
    }
}
