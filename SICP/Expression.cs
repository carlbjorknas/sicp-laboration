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

public class NumberExpression : Expression
{
    public NumberExpression(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public override string ToString() => Value.ToString();
}

public class VariableExpression : Expression
{
    public VariableExpression(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}

public abstract class PrimitiveProcedure : Expression
{
    public override string ToString() => "PrimitiveProcedure";
}

public class PrimitiveProcedurePlus : PrimitiveProcedure
{
    public static PrimitiveProcedurePlus Instance { get; } = new PrimitiveProcedurePlus();
}
