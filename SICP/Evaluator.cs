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
        else if (expression is ProcedureCallExpression call)
        {
            var evaluatedOperator = Eval(call.Operator, env);
            var evaluatedOperands = call.Operands.Select(x => Eval(x, env)).ToList();
            return Apply(evaluatedOperator, evaluatedOperands, env);
        }

        throw new Exception($"Can not evaluate the expression '{expression}'");
    }

    private bool IsSelfEvaluating(Expression expr)
        => expr is BooleanExpression or NumberExpression;

    private Expression Apply(Expression op, List<Expression> operands, Environment env)
    {
        if (op is PrimitiveProcedure primitiveProcedureOp)
        {
            return primitiveProcedureOp.Apply(operands, env);
        }

        throw new Exception($"'{op}' is not a procedure.");
    }

    //public EvalResult Eval(string expression, Environment env)
    //{
    //    if (IsSelfEvaluating(expression, out var evalResult))
    //        return evalResult!;

    //    if (IsVariable(expression))
    //        return env.GetValue(expression);

    //    if (IsDefinition(expression))
    //        return HandleDefinition(expression, env);

    //    if (IsApplication(expression))
    //    {            
    //        var op = Eval(GetOperator(expression), env);
    //        var operands = GetOperands(expression).ToList();
    //        return Apply(op, operands, env);
    //    }

    //    throw new Exception($"Unknown expression type.'{expression}'");
    //}

    //private bool IsDefinition(string expression)
    //{
    //    return GetOperator(expression) == "define";
    //}

    //private EvalResult HandleDefinition(string expression, Environment env)
    //{
    //    var operands = GetOperands(expression).ToList();

    //    if (operands.Count != 2)
    //        throw new Exception($"Invalid number of operands for 'define' in '{expression}'");

    //    var variableName = operands.First();
    //    var variableValue = Eval(operands.Last(), env);
    //    env.AddVariable(variableName, variableValue);

    //    return new SymbolEvalResult("ok");
    //}
}
