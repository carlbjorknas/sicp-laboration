namespace SICP.Expressions;

internal class PrimitiveProcedureAppend : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands, Environment env)
    {
        if (!operands.Any())
            return EmptyListExpression.Instance;

        var copiedLists = operands
            .Cast<PairExpression>()
            .Select(x => x.ShallowCopy())
            .ToList();

        for (var i = 0; i < copiedLists.Count - 1; i++)
        {
            var list1 = copiedLists[i];
            var list2 = copiedLists[i + 1];
            list1.LastPair.SetRight(list2);
        }

        return copiedLists.First();
    }
}
