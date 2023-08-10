namespace SICP.Expressions;

public class PrimitiveProcedurePlus : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands)
    {
        // TODO Handle operands that are not numbers.
        var sum = operands.Cast<NumberExpression>().Sum(x => x.Value);
        return new NumberExpression(sum);
    }
}
