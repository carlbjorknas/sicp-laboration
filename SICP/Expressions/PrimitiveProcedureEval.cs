namespace SICP.Expressions;

internal class PrimitiveProcedureEval : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands, Environment callerEnvironment)
    {
        // Note: unlike R5RS '(eval expr environment-specifier)' this eval takes no
        // environment argument – it always evaluates in the calling environment.
        EnsureOperandsHaveExpectedCount(operands, 1, "eval");
        return new Evaluator().Eval(operands[0], callerEnvironment);
    }
}
