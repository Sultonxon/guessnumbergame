namespace GuessingGame.Models;

public class Log
{
    public int Id { get; set; }
    public int GameId { get; set; }

    public int Step { get; set; }
    public int Number { get; set; }
    
    public int M { get; set; }
    public int P { get; set; }
}
