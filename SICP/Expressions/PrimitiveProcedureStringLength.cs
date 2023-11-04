namespace SICP.Expressions;

internal class PrimitiveProcedureStringLength : PrimitiveProcedure
{
    public static string Name => "string-length";

    public override Expression Apply(List<Expression> operands)
    {
        EnsureOperandsHaveExpectedCount(operands, 1, Name);
        var typedOperands = EnsureOperandHaveExpectedType<StringExpression>(operands);
        return new NumberExpression(typedOperands.First().Value.Length);
    }
}
