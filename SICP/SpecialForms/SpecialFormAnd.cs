﻿using SICP.Expressions;

namespace SICP.SpecialForms;

internal static class SpecialFormAnd
{
    public static bool Recognises(Expression expression) => expression.IsTaggedList("and");
    public static Expression Evaluate(Expression andExpression, Evaluator evaluator, Environment env)
    {
        var list = (PairExpression)andExpression;
        var dotNetlist = ((PairExpression)list.Cdr).ToDotNetList();
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
