using SICP.Expressions;

namespace SICP.ExtensionMethods;

internal static class IEnumerableExtensions
{
    public static PairExpression ToPairs(this IEnumerable<Expression> elements)
    {
        if (elements == null)
            throw new ArgumentNullException(nameof(elements));

        if (!elements.Any())
            return EmptyListExpression.Instance;

        var reversed = elements.Reverse().ToList();
        var pair = new PairExpression(reversed.First(), EmptyListExpression.Instance);
        reversed
            .Skip(1)
            .ToList()
            .ForEach(x => pair = new PairExpression(x, pair));

        return pair;
    }
}
