using GuessingGame.Models.Enums;
using GuessingGame.Models;
using Microsoft.EntityFrameworkCore;

namespace GuessingGame.Repositories.Interfaces;

public interface IGameRepository
{
    void Create(Game game);

    void Update(Game game);

    void Delete(int id);

    Game? Get(int id);

    IEnumerable<Game> GetAll();

}
