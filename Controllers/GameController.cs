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

        [HttpGet]
        public IActionResult StartPlay(string name)
        {
            return Ok(_gameService.Start(name));
        }

        [HttpPost]
        public IActionResult CheckAnswer(int id, int answer)
        {
            return Ok(_gameService.Try(id, answer));
        }

        [HttpGet("game/{id}")]
        public IActionResult GetGame(int id) => Ok(_gameService.Get(id));

        [HttpGet("gamers")]
        public IActionResult Gamers() => Ok(_gameService.GetGamers());
    }
}
