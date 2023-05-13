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
    static Expression IfAlternative(ListExpression list) => list.Cadddr;

    Expression EvaluateIf(ListExpression expression, Environment env)
    {
        var predicateExpression = IfPredicate(expression);
        var predicate = Eval(predicateExpression, env);
        var predBool = predicate as BooleanExpression;
        if (predBool == null)
            throw new Exception($"Predicate expression '{predicateExpression}' does not evaluate to a boolean.");

        if (predBool.Value)
            return Eval(IfConsequent(expression), env);
        else
            return Eval(IfAlternative(expression), env);
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
