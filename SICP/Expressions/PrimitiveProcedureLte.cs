namespace SICP.Expressions;

internal class PrimitiveProcedureLte : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands, Environment env)
    {
        EnsureOperandsHaveExpectedCount(operands, 2, "<=");
        var numbers= EnsureOperandHaveExpectedType<NumberExpression>(operands);

        return new BooleanExpression(numbers[0].Value <= numbers[1].Value);
    }
}
