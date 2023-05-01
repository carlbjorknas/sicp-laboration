namespace SICP;

public class Lexer
{
    public Token[] Tokenize(string text)
    {
        if (text == "true")
            return new[] { new BoolToken(true) };
        else if (text == "false")
            return new[] { new BoolToken(false) };
        else if (int.TryParse(text, out int value))
            return new[] {new NumberToken(value)}; 

        throw new Exception($"Cannot tokenize '{text}'.");
    }
}