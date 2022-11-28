using GuessingGame.Data;
using GuessingGame.Models;
using GuessingGame.Models.Enums;
using GuessingGame.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuessingGame.Repositories;

public class GameRepository : IGameRepository
{
    private readonly AppDbContext _context;

    public GameRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Create(Game game)
    {
        game.State = GameState.Created;
        _context.Add(game);
        _context.SaveChanges();
    }

    public void Update(Game game)
    {
        var entity = _context.Games.FirstOrDefault(g=> g.Id == game.Id);  
        if(entity != null)
        {
            entity.GamerId = game.GamerId;
            entity.State = game.State;
            entity.Trying = game.Trying;

            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        _context.Games.Remove(new Game { Id = id });
        _context.SaveChanges();
    }

    public Game? Get(int id) => _context.Games.Include(x => x.Gamer).FirstOrDefault(g => g.Id == id);

    public IEnumerable<Game> GetAll() => _context.Games.Include(x => x.Gamer).ToList();

}
