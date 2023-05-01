namespace SICP;

public class Parser
{
    public Expression Parse(Token[] tokens)
    {
        if (tokens.Any())
        {
            if (tokens[0].IsStartingParen)
            {
                return ParseProcedureCall(tokens);
            }
            if (tokens.First() is BoolToken bt)
            {
                return new BooleanExpression(bt.Value);
            }
            if (tokens.First() is NumberToken nt)
            {
                return new NumberExpression(nt.Value);
            }
            if (tokens.First() is IdentifierToken it)
            {
                return new VariableExpression(it.Value);
            }
        }

        throw new Exception($"Could not parse the token array [{string.Join(", ", tokens.Select(x => x.ToString()))}]");
    }

    private ProcedureCallExpression ParseProcedureCall(Token[] tokens)
    {
        if (tokens[1].IsEndingParen)
            throw new Exception("A procedure call requires an operator.");

        var op = Parse(new[] { tokens[1] });

        var operands = tokens
            .Skip(2)
            .TakeWhile(x => !x.IsEndingParen)
            .Select(x => Parse(new[] { x }))
            .ToList();

        if (!tokens[2 + operands.Count].IsEndingParen)
            throw new Exception("A procedure call must end with a parenthesis.");

        tokens = tokens.Skip(2 + operands.Count + 1).ToArray();

        return new ProcedureCallExpression(new VariableExpression("+"), operands);
    }
}