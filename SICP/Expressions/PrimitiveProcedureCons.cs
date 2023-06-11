namespace SICP.Expressions
{
    internal class PrimitiveProcedureCons : PrimitiveProcedure
    {
        public override Expression Apply(List<Expression> operands, Environment env)
        {
            EnsureOperandsHaveExpectedCount(operands, 2, "cons");

            return new ListExpression(operands[0], operands[1]);
        }
    }
}
