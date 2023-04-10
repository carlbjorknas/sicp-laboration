namespace SICP.EvalResults;

internal abstract class PrimitiveProcedureEvalResult : EvalResult
{
    public override bool IsPrimitiveProcedure => true;
    public abstract EvalResult Apply(Engine engine, List<string> operands, Environment env);
}
