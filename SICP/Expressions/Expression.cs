namespace SICP.Expressions;

public abstract class Expression
{
    public abstract override string ToString();

    public bool IsTaggedList(string tag)
        => this is PairExpression le &&
        le.Car is VariableExpression ve &&
        ve.Value == tag;
}