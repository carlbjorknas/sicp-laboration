namespace SICP;

public class Evaluator
{
    public Expression Eval(Expression expression, Environment env)
    {
        if (IsSelfEvaluating(expression))
        {
            return expression;
        }
        else if (expression is VariableExpression ve)
        {
            return env.GetValue(ve.Value);
        }
        else if (IsAssignment(expression))
        {
            return EvaluateAssignment((ListExpression)expression, env);
        }
        else if (IsIf(expression))
        {
            return EvaluateIf((ListExpression)expression, env);
        }
        else if (IsAnd(expression))
        {
            return EvaluateAnd((ListExpression)expression, env);
        }
        else if (expression is ListExpression list)
        {
            var evaluatedOperator = Eval(Operator(list), env);
            var evaluatedOperands = EvalOperands(Operands(list), env).ToList();
            return Apply(evaluatedOperator, evaluatedOperands, env);
        }

        throw new Exception($"Can not evaluate the expression '{expression}'");
    }

    private bool IsSelfEvaluating(Expression expr)
        => expr is BooleanExpression or NumberExpression;

    private static bool IsTaggedList(Expression expr, string tag)
        => expr is ListExpression le && le.Car is VariableExpression ve && ve.Value == tag;

    static bool IsAssignment(Expression expr) => IsTaggedList(expr, "define");
    static string AssignmentVariable(ListExpression list) => ((VariableExpression)list.Cadr).Value;
    static Expression AssignmentValue(ListExpression list) => list.Caddr;
    private Expression EvaluateAssignment(ListExpression list, Environment env)
    {
        var name = AssignmentVariable(list);
        var evaluatedValue = Eval(AssignmentValue(list), env);
        env.AddVariable(name, evaluatedValue);
        return new VariableExpression("ok");
    }

    static bool IsIf(Expression expression) => IsTaggedList(expression, "if");
    static Expression IfPredicate(ListExpression list) => list.Cadr;
    static Expression IfConsequent(ListExpression list) => list.Caddr;
    static Expression IfAlternative(ListExpression list)
        => list.Cdddr != EmptyListExpression.Instance ? list.Cadddr : new BooleanExpression(false);

    Expression EvaluateIf(ListExpression expression, Environment env)
    {
        var predicateExpression = IfPredicate(expression);
        var predicate = Eval(predicateExpression, env);

        if (predicate is BooleanExpression be && !be.Value)
            return Eval(IfAlternative(expression), env);        
        else
            return Eval(IfConsequent(expression), env);

    }

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

    static bool IsAnd(Expression expression) => IsTaggedList(expression, "and");
    Expression EvaluateAnd(ListExpression list, Environment env)
    {
        var dotNetlist = ConvertToDotNetList((ListExpression)list.Cdr);
        Expression lastEvaluatedExpression = new BooleanExpression(true);
        foreach(var expression in dotNetlist)
        {
            lastEvaluatedExpression = Eval(expression, env);
            if (lastEvaluatedExpression is BooleanExpression b && !b.Value)
                return new BooleanExpression(false);
        }
        return lastEvaluatedExpression;
    }

    static Expression Operator(ListExpression list) => list.Car;
    static ListExpression Operands(ListExpression list) => (ListExpression)list.Cdr;

    public IEnumerable<Expression> EvalOperands(ListExpression list, Environment env)
    {
        while (list != EmptyListExpression.Instance)
        {
            yield return Eval(list.Car, env);
            list = (ListExpression)list.Cdr;
        }
    }

    private Expression Apply(Expression op, List<Expression> operands, Environment env)
    {
        if (op is PrimitiveProcedure primitiveProcedureOp)
        {
            return primitiveProcedureOp.Apply(operands, env);
        }

        throw new Exception($"'{op}' is not a procedure.");
    }
}
