namespace SICP.EvalResults;

public abstract class EvalResult
{
    public virtual bool IsNumber => false;
    public virtual bool IsPrimitiveProcedure => false;
    public abstract override string ToString();
}
