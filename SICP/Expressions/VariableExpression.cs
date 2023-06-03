namespace SICP.Expressions;

public class VariableExpression : Expression
{
    public VariableExpression(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
