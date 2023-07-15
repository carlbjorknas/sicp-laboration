using SICP.Expressions;

namespace SICP;

public class Parser
{

    Token[]? _tokens;
    int _index = 0;

    public Expression Parse(Token[] tokens)
    {
        _tokens = tokens;
        _index = 0;
        return Parse();
    }

    bool MoreTokensExists => _index < _tokens!.Length - 1;
    Token CurrentToken => _tokens![_index];
    Token GetNextToken() => _tokens![++_index];

    private Expression Parse()
    {
        if (CurrentToken.IsStartingParen)
        {
            return CreateList();
        }
        if (CurrentToken.IsShorthandQuote)
        {
            GetNextToken();
            return new PairExpression(
                new VariableExpression("quote"), 
                new PairExpression(Parse(), EmptyListExpression.Instance));
        }
        if (CurrentToken is BoolToken bt)
        {
            return new BooleanExpression(bt.Value);
        }
        if (CurrentToken is NumberToken nt)
        {
            return new NumberExpression(nt.Value);
        }
        if (CurrentToken is IdentifierToken it)
        {
            return new VariableExpression(it.Value);
        }

        throw new Exception($"Could not parse the token array [{string.Join(", ", _tokens!.Select(x => x.ToString()))}]");
    }

    private PairExpression CreateList()
    {
        if (!MoreTokensExists)
            throw new Exception("Expression is incorrectly ended.");

        var token = GetNextToken();

        if (token.IsEndingParen)
            return EmptyListExpression.Instance;

        return new PairExpression(Parse(), CreateList());
    }
}