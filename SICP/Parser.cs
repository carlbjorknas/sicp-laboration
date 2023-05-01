namespace SICP;

public class Parser
{

    public Expression Parse(Token[] tokens)
    {
        return InternalParse(ref tokens);
    }

    private Expression InternalParse(ref Token[] tokens)
    {
        if (tokens.Any())
        {
            if (tokens[0].IsStartingParen)
            {
                return ParseProcedureCall(ref tokens);
            }
            if (tokens.First() is BoolToken bt)
            {
                tokens = tokens[1..];
                return new BooleanExpression(bt.Value);
            }
            if (tokens.First() is NumberToken nt)
            {
                tokens = tokens[1..];
                return new NumberExpression(nt.Value);
            }
            if (tokens.First() is IdentifierToken it)
            {
                tokens = tokens[1..];
                return new VariableExpression(it.Value);
            }
        }

        throw new Exception($"Could not parse the token array [{string.Join(", ", tokens.Select(x => x.ToString()))}]");
    }

    private ProcedureCallExpression ParseProcedureCall(ref Token[] tokens)
    {
        if (tokens[1].IsEndingParen)
            throw new Exception("A procedure call requires an operator.");

        tokens = tokens[1..];
        var op = InternalParse(ref tokens);

        var operands = new List<Expression>();
        while (true)
        {
            if (!tokens.Any())
                throw new Exception("A procedure call must end with a parenthesis.");
            if (tokens[0].IsEndingParen)
            {
                tokens = tokens[1..];
                break;
            }
            operands.Add(InternalParse(ref tokens));
        }

        return new ProcedureCallExpression(op, operands);
    }
}