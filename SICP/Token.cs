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
}