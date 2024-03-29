﻿namespace SICP
{
    public abstract class Token
    {
        public abstract override string ToString();

        public bool IsStartingParen 
            => this is PunctuatorToken pt && pt.Value == "(";

        public bool IsEndingParen
            => this is PunctuatorToken pt && pt.Value == ")";

        public bool IsShorthandQuote
            => this is PunctuatorToken pt && pt.Value == "'";
    }

    public class BoolToken : Token
    {
        public BoolToken(bool value)
        {
            Value = value;
        }

        public bool Value { get; }

        public override string ToString() => $"{GetType()} {Value}";
    }

    public class NumberToken : Token
    {
        public NumberToken(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public override string ToString() => $"{GetType()} {Value}";
    }

    public class IdentifierToken : Token
    {
        public IdentifierToken(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString() => $"{GetType()} {Value}";
    }

    public class PunctuatorToken : Token
    {
        public PunctuatorToken(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString() => $"{GetType()} {Value}";
    }

    public class StringToken : Token
    {
        public StringToken(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString() => $"{GetType()} {Value}";

    }
}