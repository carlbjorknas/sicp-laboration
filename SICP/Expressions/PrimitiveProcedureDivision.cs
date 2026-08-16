namespace SICP.Expressions;

public class PrimitiveProcedureDivision : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands)
    {
        EnsureOperandsHaveMinimumCount(operands, 1, "/");
        var numberOperands = EnsureOperandHaveExpectedType<NumberExpression>(operands);

        if (numberOperands.Count == 1)
            return new NumberExpression(1 / numberOperands[0].Value);

        var result = numberOperands[0].Value;
        foreach (var operand in numberOperands.Skip(1))
            result /= operand.Value;

        return new NumberExpression(result);
    }
}
