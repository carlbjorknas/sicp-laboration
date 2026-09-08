namespace SICP.Expressions;

public abstract class ProcedureExpression : Expression
{
    // The environment is the one the call was made in. Most primitives ignore it;
    // 'eval' and 'help' need it, and compound procedures use their own closure
    // environment instead (lexical scoping).
    public abstract Expression Apply(List<Expression> arguments, Environment environment);
}
