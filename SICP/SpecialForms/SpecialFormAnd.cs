namespace SICP.SpecialForms;

internal static class SpecialFormAnd
{
    static List<Expression> ConvertToDotNetList(ListExpression list)
    {
        var dotNetList = new List<Expression>();
        while (list != EmptyListExpression.Instance)
        {
            dotNetList.Add(list.Car);
            list = (ListExpression)list.Cdr;
        }
        return dotNetList;
    }

    public static bool Recognises(Expression expression) => expression.IsTaggedList("and");
    public static Expression Evaluate(Expression andExpression, Evaluator evaluator, Environment env)
    {
        var list = (ListExpression)andExpression;
        var dotNetlist = ConvertToDotNetList((ListExpression)list.Cdr);
        Expression lastEvaluatedExpression = new BooleanExpression(true);
        foreach (var expression in dotNetlist)
        {
            lastEvaluatedExpression = evaluator.Eval(expression, env);
            if (lastEvaluatedExpression is BooleanExpression b && !b.Value)
                return new BooleanExpression(false);
        }
        return lastEvaluatedExpression;
    }
}
