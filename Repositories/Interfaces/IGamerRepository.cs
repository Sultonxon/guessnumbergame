using GuessingGame.Models;

namespace GuessingGame.Repositories.Interfaces;

public interface IGamerRepository
{
    void Create(Gamer gamer);

    void Update(Gamer gamer);

    void Delete(Gamer gamer);

    Gamer Get(int id);

    Gamer GetByName(string name);

    IEnumerable<Gamer> GetAll();
}
