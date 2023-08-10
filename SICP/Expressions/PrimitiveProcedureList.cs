namespace SICP.Expressions;

internal class PrimitiveProcedureList : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands)
    {
        PairExpression pair = EmptyListExpression.Instance;

        foreach (var operand in ((IEnumerable<Expression>)operands).Reverse()) 
        {
            pair = new PairExpression(operand, pair);
        }

        return pair;
    }
}
