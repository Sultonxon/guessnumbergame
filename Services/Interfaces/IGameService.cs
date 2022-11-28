using GuessingGame.Models;
using System.Collections;

namespace GuessingGame.Services.Interfaces;

public interface IGameService
{
    Game Start(string name);

    Check Try(int id, int guess);

    Game GetActiveGame(string name);

    Game Get(int id);

    IList GetGamers();
}
