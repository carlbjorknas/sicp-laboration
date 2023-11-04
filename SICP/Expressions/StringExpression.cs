namespace SICP.Expressions;

internal class StringExpression : Expression
{
    public StringExpression(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override string ToString()
    {
        var escapedString = Value
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"");

         return $"\"{escapedString}\"";
    }
}
