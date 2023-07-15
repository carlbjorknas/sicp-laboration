namespace SICP.Expressions;

public class EmptyListExpression : PairExpression
{
    private EmptyListExpression() : base(null, null)
    {
    }

    public static EmptyListExpression Instance { get; } = new EmptyListExpression();

    public override string ToString() => "()";

    public override PairExpression ShallowCopy() => this;
}
