using SICP.Expressions;
using SICP.SpecialForms;

namespace SICP;

public class Evaluator
{
    public Expression Eval(Expression expression, Environment env)
    {
        if (IsSelfEvaluating(expression))
            return expression;

        else if (expression is VariableExpression ve)
            return env.GetValue(ve.Value);

        else if (SpecialFormQuote.Recognises(expression))
            return SpecialFormQuote.Evaluate(expression);

        else if (SpecialFormAssignment.Recognises(expression))
            return SpecialFormAssignment.Evaluate(expression, this, env);

        else if (SpecialFormIf.Recognises(expression))
            return SpecialFormIf.Evaluate(expression, this, env);

        else if (SpecialFormAnd.Recognises(expression))
            return SpecialFormAnd.Evaluate(expression, this, env);

        else if (SpecialFormOr.Recognises(expression))
            return SpecialFormOr.Evaluate(expression, this, env);

        else if (SpecialFormLambda.Recognises(expression))
            return SpecialFormLambda.MakeProcedure(expression, env);

        else if (expression is PairExpression list)
        {
            var evaluatedOperator = Eval(Operator(list), env);
            var evaluatedOperands = EvalOperands(Operands(list), env).ToList();
            return Apply(evaluatedOperator, evaluatedOperands, env);
        }

        throw new Exception($"Can not evaluate the expression '{expression}'");
    }

    private bool IsSelfEvaluating(Expression expr)
        => expr is BooleanExpression or NumberExpression or EmptyListExpression;

    static Expression Operator(PairExpression list) => list.Car;
    static PairExpression Operands(PairExpression list) => (PairExpression)list.Cdr;

    public IEnumerable<Expression> EvalOperands(PairExpression list, Environment env)
    {
        while (list != EmptyListExpression.Instance)
        {
            yield return Eval(list.Car, env);
            list = (PairExpression)list.Cdr;
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
