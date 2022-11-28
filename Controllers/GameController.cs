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
            game.GuessNumber = 0;
            return Ok(game);
        }

        [HttpPost("{id}")]
        public IActionResult CheckAnswer(int id, [FromBody] int answer)
        {

            var result = _gameService.Try(id, answer);
            if(!result.Playing && !result.Success)
            {
                return Ok("Failed");
            }
            if (result.Success)
                return Ok("Successed");
            return Ok(result);
        }

        [HttpGet("game/{id}")]
        public IActionResult GetGame(int id)
        {
            var game = _gameService.Get(id);
            if(game != null)
                game.GuessNumber = 0;
            return Ok(game);
        }

        [HttpGet("gamers")]
        public IActionResult Gamers() => Ok(_gameService.GetGamers());
    }
}
