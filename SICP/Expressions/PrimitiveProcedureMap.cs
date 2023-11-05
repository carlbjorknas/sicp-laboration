using SICP.ExtensionMethods;

namespace SICP.Expressions;

internal class PrimitiveProcedureMap : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands)
    {
        EnsureOperandsHaveExpectedCount(operands, 2, "Map");

        var fn = operands[0] as ProcedureExpression
            ?? throw new ArgumentException("The first argument to map should be a procedure.");

        var pairs = operands[1] as PairExpression
            ?? throw new ArgumentException("The second argument to map should be a list.");

        return pairs
            .ToDotNetList()
            .Select(x => fn.Apply(new List<Expression>{ x }))
            .ToPairs();        
    }
}
