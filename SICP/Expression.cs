using System.Runtime.CompilerServices;

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
    public abstract Expression Apply(List<Expression> operands, Environment env);
    public override string ToString() => "PrimitiveProcedure";
}

public class PrimitiveProcedurePlus : PrimitiveProcedure
{
    public static PrimitiveProcedurePlus Instance { get; } = new PrimitiveProcedurePlus();

    public override Expression Apply(List<Expression> operands, Environment env)
    {
        // TODO Handle operands that are not numbers.
        var sum = operands.Cast<NumberExpression>().Sum(x => x.Value);
        return new NumberExpression(sum);
    }
}

public class PrimitiveProcedureMinus : PrimitiveProcedure
{
    public static PrimitiveProcedureMinus Instance { get; } = new PrimitiveProcedureMinus();

    public override Expression Apply(List<Expression> operands, Environment env)
    {
        // TODO Handle operands that are not numbers.

        if (!operands.Any())
            return new NumberExpression(0);

        var numberOperands = operands.Cast<NumberExpression>().ToList();

        if (operands.Count == 1)
            return new NumberExpression(-numberOperands[0].Value);

        var sum = numberOperands.Skip(1).Sum(x => x.Value);
        return new NumberExpression(numberOperands[0].Value - sum);
    }
}

public class ListExpression : Expression
{
    private readonly Expression? _left;
    private readonly Expression? _right;

    public ListExpression(Expression? left, Expression? right)
    {
        _left = left;
        _right = right;
    }

    protected ListExpression() { }

    public Expression Car => _left ?? throw new Exception("Cannot 'Car' the empty list.");
    public Expression Cdr => _right ?? throw new Exception("Cannot 'Cdr' the empty list.");
    public Expression Cadr => ((ListExpression)Cdr).Car;
    public Expression Cddr => ((ListExpression)Cdr).Cdr;
    public Expression Caddr => ((ListExpression)Cddr).Car;
    public Expression Cdddr => ((ListExpression)Cddr).Cdr;
    public Expression Cadddr => ((ListExpression)Cdddr).Car;

    public override string ToString()
        => "List";
}

public class EmptyListExpression : ListExpression
{
    private EmptyListExpression() : base(null, null) 
    {        
    }

    public static EmptyListExpression Instance { get; } = new EmptyListExpression();

    public override string ToString() => "()";
}
