using SICP.Expressions;

namespace SICP.SpecialForms;

internal static class SpecialFormLambda
{
    internal static bool Recognises(Expression expression) => expression.IsTaggedList("lambda");

    static List<string> Parameters(PairExpression list)
        => ((PairExpression)list.Cadr).ToDotNetList().Cast<VariableExpression>().Select(x => x.Value).ToList();

    static Expression Body(PairExpression list) => list.Caddr;

    public static Expression MakeProcedure(Expression expression, Environment env)
    {
        var list = (PairExpression)expression;
        return new CompoundProcedure(Parameters(list), Body(list), env);
    }
}
