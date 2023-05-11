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
}
