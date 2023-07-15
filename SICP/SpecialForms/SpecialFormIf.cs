using SICP.Expressions;

namespace SICP.SpecialForms;

static internal class SpecialFormIf
{
    static public bool Recognises(Expression expression) => expression.IsTaggedList("if");
    static Expression IfPredicate(PairExpression list) => list.Cadr;
    static Expression IfConsequent(PairExpression list) => list.Caddr;
    static Expression IfAlternative(PairExpression list)
        => list.Cdddr != EmptyListExpression.Instance 
            ? list.Cadddr 
            : new BooleanExpression(false);

    static public Expression Evaluate(Expression expression, Evaluator evaluator, Environment env)
    {
        var list = (PairExpression)expression;
        var predicateExpression = IfPredicate(list);
        var predicate = evaluator.Eval(predicateExpression, env);

        if (predicate is BooleanExpression be && !be.Value)
            return evaluator.Eval(IfAlternative(list), env);
        else
            return evaluator.Eval(IfConsequent(list), env);

    }
}
