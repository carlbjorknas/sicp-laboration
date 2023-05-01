namespace SICP;

public class Lexer
{
    public Token[] Tokenize(string text)
    {
        var tokens = new List<Token>();
        while (text.Length > 0)
        {
            if (text.StartsWith("(") || text.StartsWith(")"))
            {
                tokens.Add(new PunctuatorToken(text[..1]));
                text = text[1..];
            }
            else if (text == "true")
                return new[] { new BoolToken(true) };
            else if (text == "false")
                return new[] { new BoolToken(false) };
            else if (int.TryParse(text, out int value))
                return new[] { new NumberToken(value) };
            else if (text.StartsWith("+"))
            {
                tokens.Add(new IdentifierToken(text[..1]));
                text = text[1..];
            }
            else
                throw new Exception($"Cannot tokenize '{text}'.");
        }

        return tokens.ToArray();
    }
}