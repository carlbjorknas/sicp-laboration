namespace SICP.Expressions;

public class PrimitiveProcedureMultiplication : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands, Environment env)
    {        
        var values = EnsureOperandHaveExpectedType<NumberExpression>(operands)
            .Select(x => x.Value)
            .ToList();

        var result = values.Aggregate(1, (a, b) => a * b);

        return new NumberExpression(result);
    }
}
