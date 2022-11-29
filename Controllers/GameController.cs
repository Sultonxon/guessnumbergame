using GuessingGame.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("Play/{name}")]
        public IActionResult StartPlay(string name)
        {
            var game = _gameService.Start(name);
            return Ok(game);
        }

        [HttpPost("{id}")]
        public IActionResult CheckAnswer(int id, [FromBody] int answer)
        {

            var result = _gameService.Try(id, answer);
            
            return Ok(result);
        }

        [HttpGet("game/{id}")]
        public IActionResult GetGame(int id)
        {
            var game = _gameService.Get(id);
            return Ok(game);
        }

        [HttpGet("gamers")]
        public IActionResult Gamers() => Ok(_gameService.GetGamers());

        [HttpGet("liders/{minWons}")]
        public IActionResult GetLiders(int minWons) => Ok(_gameService.GetLiders(minWons));

        [HttpGet("lider/{id}")]
        public IActionResult GetLider(int id)=> Ok(_gameService.GetLider(id));

        [HttpGet("gamelog/{gameId}")]
        public IActionResult GetGameLogs(int gameId)
        {
            return Ok(_gameService.GetGameLogs(gameId)??new List<Models.Log>());
        }
    }
}
