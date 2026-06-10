namespace SICP.Expressions;

public class PrimitiveProcedureDivision : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands)
    {
        var numberOperands = EnsureOperandHaveExpectedType<NumberExpression>(operands);

        var result = numberOperands[0].Value;
        foreach (var operand in numberOperands.Skip(1))
            result /= operand.Value;

        return new NumberExpression(result);
    }
}
