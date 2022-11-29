namespace GuessingGame.Models;

public class Gamer
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public IEnumerable<Game>? Games { get; set; }
}
