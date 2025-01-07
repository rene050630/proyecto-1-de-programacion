
public class Player
{
    public Token token;
    public string name;
    public Player(string name)
    {
        this.name = name;
        token = new Token("", 1, 3, Powers.sprint, 1);
    }
}