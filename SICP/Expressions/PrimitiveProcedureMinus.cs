namespace SICP.Expressions;

public class PrimitiveProcedureMinus : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands, Environment env)
    {
        // TODO Handle operands that are not numbers.

        if (!operands.Any())
            return new NumberExpression(0);

        var numberOperands = operands.Cast<NumberExpression>().ToList();

        if (operands.Count == 1)
            return new NumberExpression(-numberOperands[0].Value);

        var sum = numberOperands.Skip(1).Sum(x => x.Value);
        return new NumberExpression(numberOperands[0].Value - sum);
    }
}
