namespace SICP;

public class Parser
{
    public Expression Parse(Token[] tokens)
    {
        if (tokens.Any() && tokens.First() is BoolToken bt)
        {
            return new BooleanExpression(bt.Value);
        }

        throw new Exception();
    }
}