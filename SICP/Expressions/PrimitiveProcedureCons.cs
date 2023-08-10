namespace SICP.Expressions
{
    internal class PrimitiveProcedureCons : PrimitiveProcedure
    {
        public override Expression Apply(List<Expression> operands)
        {
            EnsureOperandsHaveExpectedCount(operands, 2, "cons");

            return new PairExpression(operands[0], operands[1]);
        }
    }
}
