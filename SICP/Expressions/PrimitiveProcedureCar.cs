namespace SICP.Expressions;

internal class PrimitiveProcedureCar : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands)
    {
        EnsureOperandsHaveExpectedCount(operands, 1, "car");
        EnsureOperandHaveExpectedType<PairExpression>(operands);

        return operands.Cast<PairExpression>().Single().Car;
    }
}
