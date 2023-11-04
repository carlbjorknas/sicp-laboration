using System.Text.RegularExpressions;

namespace SICP;

public class Lexer
{
    private readonly IReader _reader;
    private readonly Queue<Token> _tokens = new();

    public Lexer(IReader reader)
    {
        _reader = reader;
    }

    public Token GetNextToken()
    {
        while (!_tokens.Any())
        {
            var input = _reader.Read();
            Tokenize(input).ToList().ForEach(_tokens.Enqueue);
        }

        return _tokens.Dequeue();
    }

    private Token[] Tokenize(string text)
    {
        var tokens = new List<Token>();
        while (text.Length > 0)
        {
            if (string.IsNullOrWhiteSpace(text[..1]))
            {
                text = text[1..];
                continue;
            }
            if (text.StartsWith("(") || text.StartsWith(")") || text.StartsWith("'"))
            {
                tokens.Add(new PunctuatorToken(text[..1]));
                text = text[1..];
                continue;
            }
            if (text.StartsWith('"'))
            {
                var stringValue = GetStringToken(ref text);
                tokens.Add(new StringToken(stringValue));
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
            // A little bit of cheating here, a variable name can't start with "+" or "-",
            // but it should be able to have it in its name after the first char, but I ignore that for now.
            else if (tokenText == "+" || tokenText == "-" || tokenText.BeginsWithValidVariableChar())
            {
                tokens.Add(new IdentifierToken(tokenText));
            }
            else
                throw new Exception($"Cannot tokenize '{tokenText}' followed by '{text}'.");
        }

        return tokens.ToArray();
    }

    private static string GetStringToken(ref string text)
    {
        for (var i = 1; i < text.Length; i++)
        {
            if (text[i] == '"')
            {
                var stringValue = text[1..i];
                text = text[(i + 1)..];
                return stringValue;
            }
        }
        throw new Exception($"Invalid string in '{text}'.");
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

    public static bool BeginsWithValidVariableChar(this string text)
        => Regex.IsMatch(text[..1], "[a-z><*=]", RegexOptions.IgnoreCase);

    public static string TakeUntilNextPunctuation(this string text)
    {
        var chars = text
            .TakeWhile(x => x is not (' ' or '(' or ')'))
            .ToArray();
        return new string(chars);
    }
}