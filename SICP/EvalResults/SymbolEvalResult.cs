namespace SICP.EvalResults;

internal class SymbolEvalResult : EvalResult
{
    public SymbolEvalResult(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
