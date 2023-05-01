namespace SICP;

public class Parser
{
    public Expression Parse(Token[] tokens)
    {
        if (tokens.Any())
        {
            if (tokens.First() is BoolToken bt)
            {
                return new BooleanExpression(bt.Value);
            }
            if (tokens.First() is NumberToken nt)
            {
                return new NumberExpression(nt.Value);
            }
        }

        throw new Exception($"Could not parse the token array [{string.Join(", ", tokens.Select(x => x.ToString()))}]");
    }
}