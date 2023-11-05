namespace SICP.Expressions;

public abstract class ProcedureExpression : Expression
{
    public abstract Expression Apply(List<Expression> arguments);
}
