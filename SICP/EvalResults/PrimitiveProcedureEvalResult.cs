namespace SICP.EvalResults;

internal abstract class PrimitiveProcedureEvalResult : EvalResult
{
    public override bool IsPrimitiveProcedure => true;
    public abstract EvalResult Apply(Evaluator engine, List<string> operands, Environment env);
}
