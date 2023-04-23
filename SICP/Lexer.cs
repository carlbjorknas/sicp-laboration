namespace SICP;

public class Lexer
{
    public Token[] Tokenize(string text)
    {
        if (text == "true")
            return new[] { new BoolToken(true) };
        else if (text == "false")
            return new[] { new BoolToken(false) };

        throw new Exception("");
    }
}