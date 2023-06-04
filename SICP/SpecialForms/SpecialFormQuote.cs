using SICP.Expressions;

namespace SICP.SpecialForms;

internal static class SpecialFormQuote
{
    public static bool Recognises(Expression expression) => expression.IsTaggedList("quote");

    public static Expression Evaluate(Expression expression)
    {
        return ((ListExpression)expression).Cadr;
    }
}
