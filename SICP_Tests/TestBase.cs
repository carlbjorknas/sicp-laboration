using FluentAssertions;
using SICP;
using System;
using System.Linq;

namespace SICP_Tests;

public class TestBase
{
    protected ListExpression CreateList(params Expression[] expressions)
    {
        ListExpression list = EmptyListExpression.Instance;

        foreach (var expression in expressions.Reverse())
        {
            list = new ListExpression(expression, list);
        }

        return list;
    }

    protected bool CompareLists(Expression actual, ListExpression expected)
    {
        actual.Should().NotBeNull().And.BeAssignableTo<ListExpression>();
        var actualList = (ListExpression)actual;

        if (expected == EmptyListExpression.Instance)
        {
            actualList.Should().BeSameAs(expected);
        }
        else
        {
            CompareExpressions(actualList.Car, expected.Car);
            CompareExpressions(actualList.Cdr, expected.Cdr);
        }
        return true;
    }

    protected bool CompareExpressions(Expression actual, Expression expected) => expected switch
    {
        ListExpression le => CompareLists(actual, le),
        BooleanExpression be1 => actual is BooleanExpression be2 && be1.Value == be2.Value,
        NumberExpression ne1 => actual is NumberExpression ne2 && ne1.Value == ne2.Value,
        VariableExpression ve1 => actual is VariableExpression ve2 && ve1.Value == ve2.Value,
        _ => throw new System.NotImplementedException()
    };
}
