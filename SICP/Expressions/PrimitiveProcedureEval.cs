namespace SICP.Expressions;

internal class PrimitiveProcedureEval : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands, Environment env)
    {
        var evaluator = new Evaluator();
        return evaluator.Eval(operands[0], new Environment());
    }
}
