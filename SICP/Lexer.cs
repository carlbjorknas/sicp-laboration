using System.Text.RegularExpressions;

namespace SICP;

public class Lexer
{
    public Token[] Tokenize(string text)
    {
        var tokens = new List<Token>();
        while (text.Length > 0)
        {
            if (text.StartsWith(" "))
            {
                text = text[1..];
            }
            else if (text.StartsWith("(") || text.StartsWith(")"))
            {
                tokens.Add(new PunctuatorToken(text[..1]));
                text = text[1..];
            }
            else if (text == "true")
                return new[] { new BoolToken(true) };
            else if (text == "false")
                return new[] { new BoolToken(false) };
            else if (text.BeginsWithDigit())
            {
                tokens.Add(GetNumberToken(ref text));
            }
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

    private NumberToken GetNumberToken(ref string text)
    {
        var tokenText = text.TakeUntilNextPunctuation();
        if (int.TryParse(tokenText, out var number))
        {            
            text = text[tokenText.Length..];
            return new NumberToken(number);
        }

        throw new Exception($"'{tokenText}' is not a valid number.");
    }
}

internal static class StringExtensions
{
    public static bool BeginsWithDigit(this string text)
        => Regex.IsMatch(text[..1], "[0-9]");

    public static string TakeUntilNextPunctuation(this string text)
    {
        var chars = text
            .TakeWhile(x => x is not (' ' or '(' or ')'))
            .ToArray();
        return new string(chars);
    }
}