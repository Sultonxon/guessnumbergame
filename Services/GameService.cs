using GuessingGame.Data;
using GuessingGame.Models;
using GuessingGame.Repositories.Interfaces;
using GuessingGame.Services.Interfaces;
using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
using System.Collections;

namespace GuessingGame.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IGamerRepository _gamerRepository;
    private readonly AppDbContext _conterxt;

    public GameService(IGameRepository gameRepository, IGamerRepository gamerRepository, AppDbContext context)
    {
        _gameRepository = gameRepository;
        _gamerRepository = gamerRepository;
        _conterxt = context;
    }


    public Game Start(string name)
    {
        Gamer gamer = _gamerRepository.GetByName(name);
        if(gamer == null)
        {
            _gamerRepository.Create(new Gamer { Name = name });
            gamer = _gamerRepository.GetByName(name);
        }

        Game newGame = new Game { GamerId = gamer.Id, GuessNumber = GenerateNumber(4)};

        _gameRepository.Create(newGame);
        Console.WriteLine($"=============>{newGame.GuessNumber}");

        return newGame;
    }

    private int GenerateNumber(int numberOfDigits)
    {

        List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var n = 0;
        int d;
        d = list[Random.Shared.Next(0, list.Count - 1)];
        n = n * 10 + d;
        list.Remove(d);
        list.Add(0);
        for (int i = 1; i < numberOfDigits; i++)
        {
            d = list[Random.Shared.Next(0, list.Count - 1)];
            n = n * 10 + d;
            list.Remove(d);
        }

        return n;
    }

    public Game GetActiveGame(string name)
    {
        Gamer gamer = _gamerRepository.GetByName(name);
        if (gamer == null)
        {
            return null;
        }
        Game activeGame = gamer.Games.FirstOrDefault(x => x.State == Models.Enums.GameState.Playing
            || x.State == Models.Enums.GameState.Created);
        return activeGame;


    }

    public Check Try(int id, int guess)
    {       
        var game = _gameRepository.Get(id);
        Console.WriteLine("]]]]]]]]]]]]" + game.GuessNumber);
        

        if (guess > 9999 || guess < 1000) return new Check { M = 0, P= 0 };
        game.GuessNumber = _gameRepository.Get(game.Id).GuessNumber;
        game.Trying++;
        var guessDigits = Digits(guess);
        var secretDigits = Digits(game.GuessNumber) ;
        Check result = new Check();
        
        for (int i = 0; i < 4; i++)
        {
            if (guessDigits[i] == secretDigits[0] || guessDigits[i] == secretDigits[1]
                || guessDigits[i] == secretDigits[2] || guessDigits[i] == secretDigits[3])
                result.M++;
        }

        for (int i = 0; i < 4; i++)
        {
            if (guessDigits[i] == secretDigits[i])
                result.P++;
        }

        _conterxt.Logs.Add(new Log { GameId = game.Id, Step = game.Trying, Number = guess, M = result.M, P = result.P });
        _conterxt.SaveChanges();
        result.Logs = _conterxt.Logs.Where(x => x.GameId == game.Id).ToList();
        if (result.P == 4)
            game.State = Models.Enums.GameState.Completed;
        if (result.P != 4 && game.Trying >= 8)
            game.State = Models.Enums.GameState.Failed;
        if (result.P != 4 && game.Trying < 8)
            game.State = Models.Enums.GameState.Playing;

        _gameRepository.Update(game);
        
        if(game.State == Models.Enums.GameState.Completed)
        {
            result.Success = true;
            result.Playing = false;
        }
        return result;        
    }



    private List<int> Digits(int n)
    {
        List<int> digs  = new List<int>();
        do
        {
            digs.Add(n % 10);
            n = n/ 10;
        } while (n > 0);
        digs.Reverse();
        return digs;
    }

    public Game Get(int id)
    {
        return _gameRepository.Get(id);
    }

    public IList GetGamers() => _gamerRepository.GetAll().Select(x => new
    {
        x.Id,
        x.Name,
        CompletedGames = x.Games.Count(y => y.State == Models.Enums.GameState.Completed),
        FailedGames = x.Games.Count(y => y.State == Models.Enums.GameState.Failed),
        NotCompletedGames = x.Games.Count(y => y.State == Models.Enums.GameState.Created ||
                                               y.State == Models.Enums.GameState.Ended ||
                                               y.State == Models.Enums.GameState.Playing)
    }).ToList();

    public IList GetLiders(int minWons = 0) => _gamerRepository.GetAll()
        .Where(x => x.Games is null ? false : x.Games.Where(y => y.State == Models.Enums.GameState.Completed).Any())
        .Select(x => new 
                {
                    x.Id,
                    x.Name, 
                    GameCount = x.Games.Count(x => x.State == Models.Enums.GameState.Completed),
                    Record = x.Games.Any(y => y.State== Models.Enums.GameState.Completed)
                                ? x.Games.Where(y => y.State == Models.Enums.GameState.Completed).MinBy(y => y.Trying):null
                }
        ).Where(x => x.GameCount >= minWons).ToList();

    public Object GetLider(int id)
    {
        var gamer = _gamerRepository.Get(id);
        return new { gamer.Id, gamer.Name, gamer.Games };
    }

    public List<Log> GetGameLogs(int gameId) => _conterxt.Logs.Where(x => x.GameId == gameId).ToList();
}
