namespace SICP.Expressions;

internal class PrimitiveProcedureCdr : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands, Environment env)
    {
        EnsureOperandsHaveExpectedCount(operands, 1, "cdr");
        EnsureOperandHaveExpectedType<PairExpression>(operands);

        return operands.Cast<PairExpression>().Single().Cdr;
    }
}
