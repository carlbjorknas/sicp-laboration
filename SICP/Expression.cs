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

public class ProcedureCallExpression : Expression
{
    public ProcedureCallExpression(Expression op, List<Expression>? operands)
    {
        Operator = op;
        Operands = operands ?? new List<Expression>();
    }

    public Expression Operator { get; }
    public List<Expression> Operands { get; }

    public override string ToString() => $"Procedure call. Operator: '{Operator}'. {Operands.Count} operands.";
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
        //if (!operands.Any())
            return new NumberExpression(0);

        //if (operands.Count == 1)
        //    return new IntEvalResult(-((IntEvalResult)evaluatedOperands[0]).Value);

        //var sum = evaluatedOperands.Skip(1).Cast<IntEvalResult>().Sum(x => x.Value);
        //return new IntEvalResult(((IntEvalResult)evaluatedOperands[0]).Value - sum);
    }
}
