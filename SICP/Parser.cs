using SICP.Expressions;

namespace SICP;

public class Parser
{
    private readonly Lexer _lexer;

    public Parser(Lexer lexer)
    {
        _lexer = lexer;
    }

    public Expression GetNextExpression()
    {
        GetNextToken();
        return Parse();
    }

    Token? CurrentToken { get; set; }
    Token GetNextToken() => CurrentToken = _lexer.GetNextToken();

    private Expression Parse()
    {
        if (CurrentToken!.IsStartingParen)
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

        throw new Exception($"Could not parse the token {CurrentToken}");
    }

    private PairExpression CreateList()
    {
        var token = GetNextToken();

        if (token.IsEndingParen)
            return EmptyListExpression.Instance;

        return new PairExpression(Parse(), CreateList());
    }
}