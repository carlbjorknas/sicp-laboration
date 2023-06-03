namespace SICP.Expressions;

public class EmptyListExpression : ListExpression
{
    private EmptyListExpression() : base(null, null)
    {
    }

    public static EmptyListExpression Instance { get; } = new EmptyListExpression();

    public override string ToString() => "()";
}
