﻿using SICP.Expressions;

namespace SICP.SpecialForms;

internal static class SpecialFormOr
{
    public static bool Recognises(Expression expression) => expression.IsTaggedList("or");
    public static Expression Evaluate(Expression orExpression, Evaluator evaluator, Environment env)
    {
        var list = (ListExpression)orExpression;
        var dotNetlist = ((ListExpression)list.Cdr).AsFlatDotNetList();
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
