using LightsOut.Engine.Interfaces;
using LightsOut.Server.Models.Binding;
using LightsOut.Server.Models.View;
using LightsOut.Utility;
using Microsoft.AspNetCore.Mvc;

namespace LightsOut.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LightsOutController : ControllerBase
    {
        private readonly ILogger<LightsOutController> logger;
        private readonly ILightsOutEngine lightsOutEngine;

        public LightsOutController(ILogger<LightsOutController> logger,
            ILightsOutEngine lightsOutEngine)
        {
            this.logger = logger;
            this.lightsOutEngine = lightsOutEngine;
        }

        [HttpPost]
        public async Task<LightsOutViewModel> IntializeAsync([FromBody] InitializeBindingModel model)
        {
            logger.LogInformation("Intializing Lights Out Game");

            var game = await lightsOutEngine.IntializeAsync(model.RowLegth, model.ColumnLegth, model.ActiveCells);

            logger.LogInformation("Lights Out Game Intialized!");

            return new LightsOutViewModel
            {
                Id = game.Id,
                Board = game.Board.ToJaggedArray(),
                Score = game.Score,
                IsSolved = game.IsSolved,
            };
        }

        [HttpGet()]
        [Route("{Id}")]
        public async Task<LightsOutViewModel> Toggle([FromRoute]Guid Id)
        {
            logger.LogInformation("Obtaining Lights Out Game");

            var game = await lightsOutEngine.GetAsync(Id);

            logger.LogInformation("Lights Out Game Obtained");

            return new LightsOutViewModel
            {
                Id = game.Id,
                Board = game.Board.ToJaggedArray(),
                Score = game.Score,
                IsSolved = game.IsSolved,
            };
        }

        [HttpPatch]
        public async Task<LightsOutViewModel> Toggle(ToggleBindingModel model)
        {
            logger.LogInformation("Toggling Switch");

            var game = await lightsOutEngine.ToggleAsync(model.Id, model.GetArrayIndex().Alpha, model.GetArrayIndex().Numeric);

            logger.LogInformation("Switch Toggled");

            return new LightsOutViewModel
            {
                Id = game.Id,
                Board = game.Board.ToJaggedArray(),
                Score = game.Score,
                IsSolved = game.IsSolved,
            };
        }
    }
}