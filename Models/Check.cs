namespace GuessingGame.Models;

public class Check
{
    public int M { get; set; }
    public int P { get; set; }
    public bool Playing { get; set; } = true;
    public bool Success { get; set; }
    public IEnumerable<Log> Logs { get; set; }
}
