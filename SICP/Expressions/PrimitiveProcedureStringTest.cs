namespace SICP.Expressions;

internal class PrimitiveProcedureStringTest : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands, Environment environment)
    {
        EnsureOperandsHaveExpectedCount(operands, 1, "string?");
        return new BooleanExpression(operands.First() is StringExpression);
    }
}
