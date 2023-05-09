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
                if (IsDefinition(tokens))
                {
                    return ParseDefinition(ref tokens);
                }
                tokens = tokens[1..];
                return CreateList(ref tokens);
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

    private bool IsDefinition(Token[] tokens)
        => tokens.Length > 1 && 
        tokens[1] is IdentifierToken identifier && 
        identifier.Value == "define";

    /// <summary>
    /// For example (define x 10)
    /// </summary>
    /// <param name="tokens"></param>
    /// <returns></returns>
    private DefinitionExpression ParseDefinition(ref Token[] tokens)
    {
        tokens = tokens[2..]; // Remove "(define"

        if (!tokens.Any())
            throw new Exception("Too few operands to 'define'.");

        if (tokens.First() is not IdentifierToken identifierToken)
            throw new Exception("Bad format in define.");
        
        tokens = tokens[1..];
        var value = InternalParse(ref tokens);

        return new DefinitionExpression(identifierToken.Value, value);
    }

    private ListExpression CreateList(ref Token[] tokens)
    {
        if (!tokens.Any())
            throw new Exception("Expression is incorrectly ended.");

        if (tokens.First().IsEndingParen)
            return EmptyListExpression.Instance;

        if (tokens.First().IsStartingParen)
        {
            var rest = tokens[1..];
            var left = CreateList(ref rest);
            var right = CreateList(ref rest);
            return new ListExpression(left, right);
        }
        else
        {
            var first = tokens[..1];
            var rest = tokens[1..];
            return new ListExpression(InternalParse(ref first), CreateList(ref rest));
        }
    }
}