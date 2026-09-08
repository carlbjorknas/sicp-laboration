using SICP.Expressions;

namespace SICP.SpecialForms;

internal static class SpecialFormQuote
{
    public const string Tag = "quote";

    public static bool Recognises(Expression expression) => expression.IsTaggedList(Tag);

    public static Expression Evaluate(Expression expression)
    {
        return ((PairExpression)expression).Cadr;
    }
}
