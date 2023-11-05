using SICP.Expressions;

namespace SICP.SpecialForms;

internal static class SpecialFormOr
{
    public static bool Recognises(Expression expression) => expression.IsTaggedList("or");
    public static Expression Evaluate(Expression orExpression, Evaluator evaluator, Environment env)
    {
        var list = (PairExpression)orExpression;
        var dotNetlist = ((PairExpression)list.Cdr).ToDotNetList();
        Expression? lastEvaluatedExpression;
        foreach (var expression in dotNetlist)
        {
            lastEvaluatedExpression = evaluator.Eval(expression, env);
            if ((lastEvaluatedExpression is BooleanExpression b && b.Value) || 
                lastEvaluatedExpression is not BooleanExpression)
            {
                // The first value that is not 'false' is returned.
                return lastEvaluatedExpression;
            }

        }
        return new BooleanExpression(false);
    }
}
