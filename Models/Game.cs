using GuessingGame.Models.Enums;

namespace GuessingGame.Models;

public class Game
{
    public int Id { get; set; }

    public int GuessNumber { get; set; }

    public int GamerId { get; set; }
    public Gamer? Gamer { get; set; }

    public GameState State { get; set; }

    public int Trying { get; set; }

}
