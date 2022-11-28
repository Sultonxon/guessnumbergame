using GuessingGame.Data;
using GuessingGame.Models;
using GuessingGame.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuessingGame.Repositories;

public class GamerRepository : IGamerRepository
{
    private readonly AppDbContext _context;

    public GamerRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Create(Gamer gamer)
    {
        _context.Gamers.Add(gamer);
        _context.SaveChanges();
    }

    public void Delete(Gamer gamer)
    {
        _context.Remove(new Gamer { Id = gamer.Id });
        _context.SaveChanges();
    }

    public Gamer? Get(int id) => _context.Gamers.Include(x => x.Games).FirstOrDefault(x => x.Id == id);


    public Gamer? GetByName(string name) => _context.Gamers.Include(x => x.Games).FirstOrDefault(x => x.Name == name);

    public IEnumerable<Gamer> GetAll() => _context.Gamers.Include(x => x.Games).ToList();

    public void Update(Gamer gamer)
    {
        var entity = _context.Gamers.FirstOrDefault(x => x.Id == gamer.Id);
        if(entity != null)
        {
            entity.Name = gamer.Name;
            _context.SaveChanges();
        }
    }
}
