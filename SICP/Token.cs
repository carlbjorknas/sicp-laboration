namespace SICP
{
    public abstract class Token
    {
        public abstract override string ToString();
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
}