namespace SICP
{
    public class Token
    {
    }

    public class BoolToken : Token
    {
        public BoolToken(bool value)
        {
            Value = value;
        }

        public bool Value { get; }
    }

    public class NumberToken : Token
    {
        public NumberToken(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}