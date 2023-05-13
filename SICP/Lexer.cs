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
                continue;
            }
            if (text.StartsWith("(") || text.StartsWith(")"))
            {
                tokens.Add(new PunctuatorToken(text[..1]));
                text = text[1..];
                continue;
            }

            var tokenText = text.TakeUntilNextPunctuation();
            text = text[tokenText.Length..];

            if (tokenText == "true")
                tokens.Add(new BoolToken(true));
            else if (tokenText == "false")
                tokens.Add(new BoolToken(false));
            else if (tokenText.BeginsWithDigit())
            {
                tokens.Add(GetNumberToken(tokenText));
            }
            else if (tokenText == "+" || tokenText == "-" || tokenText.BeginsWithLetter())
            {
                tokens.Add(new IdentifierToken(tokenText));
            }
            else
                throw new Exception($"Cannot tokenize '{text}'.");
        }

        return tokens.ToArray();
    }

    private NumberToken GetNumberToken(string tokenText)
    {
        if (int.TryParse(tokenText, out var number))
        {            
            return new NumberToken(number);
        }

        throw new Exception($"'{tokenText}' is not a valid number.");
    }
}

internal static class StringExtensions
{
    public static bool BeginsWithDigit(this string text)
        => Regex.IsMatch(text[..1], "[0-9]");

    public static bool BeginsWithLetter(this string text)
        => Regex.IsMatch(text[..1], "[a-z]", RegexOptions.IgnoreCase);

    public static string TakeUntilNextPunctuation(this string text)
    {
        var chars = text
            .TakeWhile(x => x is not (' ' or '(' or ')'))
            .ToArray();
        return new string(chars);
    }
}