using SICP.ExtensionMethods;

namespace SICP.Expressions;

internal class PrimitiveProcedureMap : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands)
    {
        if (operands.Count < 2)
            throw new ArgumentException($"'map' takes at least 2 operands.");

        var fn = operands[0] as ProcedureExpression
            ?? throw new ArgumentException("The first argument to 'map' should be a procedure.");

        var theLists = EnsureOperandHaveExpectedType<PairExpression>(operands.Skip(1).ToList())
            .Select(x => x.ToDotNetList())
            .ToList();

        var lengthOfShortestList = theLists.Select(x => x.Count).Min();
        
        return Enumerable.Range(0, lengthOfShortestList)
            .Select(index =>
            {
                var elementsHavingSameIndex = theLists.Select(x => x[index]).ToList();
                return fn.Apply(elementsHavingSameIndex);
            })
            .ToPairs();
    }
}
