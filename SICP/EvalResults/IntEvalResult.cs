namespace SICP.EvalResults;

internal class IntEvalResult : EvalResult
{
    public IntEvalResult(int value)
    {
        Value = value;
    }

    public override bool IsNumber => true;
    public int Value { get; }

    public override string ToString() => Value.ToString();
}
