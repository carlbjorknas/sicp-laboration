namespace SICP.Expressions;

internal class PrimitiveProcedureAppend : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands, Environment env)
    {
        if (!operands.Any())
            return EmptyListExpression.Instance;

        var allButLastOperand = operands
            .Take(operands.Count - 1)
            .ToList();
        
        var operandsOfWrongType = allButLastOperand
            .Where(x => x is not PairExpression)
            .ToList();
        if (operandsOfWrongType.Any())
            throw new Exception("The method 'append' requires all but the last argument to be of type 'list'.");

        var copiedLists = allButLastOperand
            .Cast<PairExpression>()
            .Where(x => x is not EmptyListExpression)
            // TODO Is shallow enough or need to be deep copy?
            .Select(x => x.ShallowCopy()) 
            .ToList();

        // Set cdr of the last pair in each list to point to the next list.
        copiedLists.Zip(copiedLists.Skip(1))
            .ToList()
            .ForEach(zipPair => zipPair.First.LastPair.SetRight(zipPair.Second));

        var appendedList = copiedLists.First();
        appendedList.LastPair.SetRight(operands.Last());
        return appendedList;
    }
}
