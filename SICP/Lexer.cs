namespace SICP;

public class Lexer
{
    public Token[] Tokenize(string text)
    {
        if (text == "true")
            return new[] { new BoolToken(true) };

        throw new Exception("");
    }
}