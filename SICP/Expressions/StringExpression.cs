namespace SICP.Expressions;

internal class StringExpression : Expression
{
    public StringExpression(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => $"\"{Value}\"";
}
