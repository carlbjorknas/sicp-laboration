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
        else if (IsDefinition(expression))
        {
            return HandleDefinition((ListExpression)expression, env);
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

    static bool IsDefinition(Expression expr) => IsTaggedList(expr, "define");
    static string DefinitionVariable(ListExpression list) => ((VariableExpression)list.Cadr).Value;
    static Expression DefinitionValue(ListExpression list) => list.Caddr;

    static Expression Operator(ListExpression list) => list.Car;
    static ListExpression Operands(ListExpression list) => (ListExpression)list.Cdr;

    private Expression HandleDefinition(ListExpression list, Environment env)
    {
        var name = DefinitionVariable(list);
        var evaluatedValue = Eval(DefinitionValue(list), env);
        env.AddVariable(name, evaluatedValue);
        return new VariableExpression("ok");
    }

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
