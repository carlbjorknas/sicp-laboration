namespace SICP;

public abstract class Expression
{
    public abstract override string ToString();
}

public class BooleanExpression : Expression
{
    public BooleanExpression(bool value)
    {
        Value = value;
    }

    public bool Value { get; }

    public override string ToString() => Value ? "true" : "false";

}