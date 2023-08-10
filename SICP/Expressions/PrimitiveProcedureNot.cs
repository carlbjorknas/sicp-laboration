namespace SICP.Expressions;

internal class PrimitiveProcedureNot : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands)
    {
        EnsureOperandsHaveExpectedCount(operands, 1, "not");

        var operandIsFalse = operands[0] is BooleanExpression b && !b.Value;
        return new BooleanExpression(operandIsFalse ? true : false);
    }
}
