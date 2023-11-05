namespace SICP.Expressions;

public abstract class PrimitiveProcedure : ProcedureExpression
{
    public override string ToString() => "PrimitiveProcedure";

    protected void EnsureOperandsHaveExpectedCount(List<Expression> operands, int expectedCount, string opName)
    {
        if (operands.Count != expectedCount)
            throw new Exception($"'{opName}' expects {expectedCount} operand(s), got {operands.Count}");
    }

    protected List<T> EnsureOperandHaveExpectedType<T>(List<Expression> operands) where T : Expression
    {
        foreach (var operand in operands)
        {
            if (!operand.GetType().IsAssignableTo(typeof(T)))
                throw new Exception($"Operand '{operand}' is not assignable to type {typeof(T)}.");
        }

        return operands.Cast<T>().ToList();
    }
}
