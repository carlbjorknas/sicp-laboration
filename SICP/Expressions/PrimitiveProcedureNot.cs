namespace SICP.Expressions;

internal class PrimitiveProcedureNot : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands, Environment env)
    {
        if (operands.Count != 1) 
            throw new Exception($"'not' expects 1 argument, got {operands.Count}");

        var operandIsFalse = operands[0] is BooleanExpression b && !b.Value;
        return new BooleanExpression(operandIsFalse ? true : false);
    }
}
