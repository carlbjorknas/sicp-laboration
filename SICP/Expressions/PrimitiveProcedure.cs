namespace SICP.Expressions;

public abstract class PrimitiveProcedure : Expression
{
    public abstract Expression Apply(List<Expression> operands, Environment env);
    public override string ToString() => "PrimitiveProcedure";
}
