namespace SICP.Expressions;

public class NumberExpression : Expression
{
    public NumberExpression(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public override string ToString() => Value.ToString();
}
